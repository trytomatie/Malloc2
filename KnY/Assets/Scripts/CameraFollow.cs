using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform conditionalTarget;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private float screenShakeTimer = 0;
    [Header("Camera Borders")]
    public bool bordersEnabled = false;
    public Vector2 point1;
    public Vector2 point2;
    private float screenShakestrength = 0.01f;

    private static CameraFollow instance;
    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.green;

        Gizmos.DrawLine(new Vector2(point2.x, point2.y), new Vector2(point2.x, point1.y));
        Gizmos.DrawLine(new Vector2(point2.x, point2.y), new Vector2(point1.x, point2.y));
        Gizmos.DrawLine(new Vector2(point1.x, point1.y), new Vector2(point2.x, point1.y));
        Gizmos.DrawLine(new Vector2(point1.x, point1.y), new Vector2(point1.x, point2.y));
#endif
    }

    void Start()
    {
        instance = this;
        if (GameObject.FindObjectOfType<MapGenerator>() != null)
        {
            bordersEnabled = false;
        }
    }
    void FixedUpdate()
    {
        if (screenShakeTimer > 0)
        {
            ShakeScreen();
            screenShakeTimer -= Time.fixedDeltaTime;
        }
        Vector2 desiredPosition = target.position + offset;
        if (conditionalTarget.gameObject.activeSelf && !Director.GetInstance().isMobile)
        {
            Vector2 mousePos = Cursor.GetWorldPositionOnPlane(Input.mousePosition, 0);
            // desiredPosition = ((Vector2)target.position + mousePos) / 2;
            Vector2 direction = PublicGameResources.CalculateNormalizedDirection(target.position, mousePos);
            Ray2D ray = new Ray2D(target.position + offset, direction);
            float distance = Vector2.Distance(target.position, mousePos);
            desiredPosition = ray.GetPoint(distance / 16);
        }
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = Camera.main.aspect * halfHeight;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        if (bordersEnabled)
        {
            Vector3 posInBorder = new Vector3(Mathf.Clamp(smoothedPosition.x, point1.x + halfWidth, point2.x - halfWidth), Mathf.Clamp(smoothedPosition.y,
                point2.y + halfHeight, point1.y - halfHeight), -10);
            smoothedPosition = Vector3.Lerp(transform.position, posInBorder, smoothSpeed);
        }
         transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
    }

    public void ActivateScreenShake(float time)
    {
        screenShakestrength = 0.01f;
        screenShakeTimer = time;
    }
    public void ActivateScreenShake(float time, float strength)
    {
        screenShakestrength = strength;
        screenShakeTimer = time;
    }

    public void ShakeScreen()
    {
        transform.position = new Vector3(transform.position.x + Random.Range(-screenShakestrength, screenShakestrength), transform.position.y + +Random.Range(-screenShakestrength, screenShakestrength), -10);
    }


    /// <summary>
    /// Sets border points for camera and enables borders
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    public static void SetAndEnableBorders(Vector2 point1, Vector2 point2)
    {
        instance.point1 = point1;
        instance.point2 = point2;
        instance.bordersEnabled = true;
    }


    /// <summary>
    /// Sets the Borderer attribute
    /// </summary>
    /// <param name="value"></param>
    public static void Borders(bool value)
    {
        instance.bordersEnabled = value;
    }
}
