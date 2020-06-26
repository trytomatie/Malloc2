using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextAnimation : MonoBehaviour {

    public int timer = 0;
    public Vector3 origin;

    private float horizontalVelocity;
    private Rigidbody2D rb;

    // 100 and 150 on "Not screen space"
    private float range = 0.05f;
    private float bounceValue = 1f;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        horizontalVelocity = Random.Range(-range, range);
        rb.velocity = new Vector2(horizontalVelocity, bounceValue);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        UpdateTextLogic();
    }

    private void UpdateTextLogic()
    {
        if(GameObject.Find("Canvas").GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceCamera)
        {
            transform.parent.GetComponent<RectTransform>().position = origin;
        }
        else
        {
            transform.parent.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(origin);
        }
        if (timer % 30 == 0)
        {
            bounceValue *= 0.8f;
            rb.velocity = new Vector2(horizontalVelocity, bounceValue);
        }
        timer++;
        if (timer == 90)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}