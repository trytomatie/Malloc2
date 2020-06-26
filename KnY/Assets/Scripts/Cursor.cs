using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public Dictionary<GameObject, bool> targetList = new Dictionary<GameObject, bool>();
    public GameObject targetConnectionPrefab;
    public GameObject targetUIPrefab;
    public List<TargetConnectionUI> connections = new List<TargetConnectionUI>();
    public List<GameObject> targetUiObjects = new List<GameObject>();

    private TargetConnectionUI currentConnection;
	// Use this for initialization
	void Start () {
    }
	
    void OnEnable()
    {
        Director.GetInstance().DarkenLevel(0.1f);
        Director.SetTimeScale(0.2f);
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnDisable()
    {
        Director.SetTimeScale(1f);
        Director.GetInstance().BrightenLevel();
    }


    // Update is called once per frame
    void Update () {
        transform.position = GetWorldPositionOnPlane(Input.mousePosition,0);
	}

    public static Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Statusmanager otherStatus = other.GetComponent<Statusmanager>();
        if(otherStatus != null && otherStatus.faction != Statusmanager.Faction.PlayerFaction && !targetList.ContainsKey(other.gameObject) && !otherStatus.isDead)
        {
            targetList.Add(other.gameObject,true);

            currentConnection.endPoint = other.transform;
            CreateTargetUI(other.transform, targetList.Count);
            CreateNewConnection(other.transform, transform);
        }
    }

    private void CreateTargetUI(Transform transform, int i)
    {
        TargetUI targetUIObject = Instantiate(targetUIPrefab, transform.position, Quaternion.identity).GetComponent<TargetUI>();
        targetUIObject.transform.parent = transform;
        targetUIObject.SetTargetSprite(i);
        targetUiObjects.Add(targetUIObject.gameObject);
    }

    public void ResetTargetList()
    {
        foreach(TargetConnectionUI t in connections)
        {
            Destroy(t.gameObject);
        }
        foreach (GameObject t in targetUiObjects)
        {
            Destroy(t.gameObject);
        }
        targetList.Clear();
        targetUiObjects.Clear();
        connections.Clear();
    }

    public void StartTargeting()
    {
        currentConnection = Instantiate(targetConnectionPrefab,new Vector2(10000,10000),Quaternion.identity).GetComponent<TargetConnectionUI>();
        currentConnection.GetComponent<TargetConnectionUI>().startPoint = GameObject.Find("Player").transform;
        currentConnection.GetComponent<TargetConnectionUI>().endPoint = transform;
        connections.Add(currentConnection);

    }

    public void CreateNewConnection(Transform start,Transform end)
    {
        currentConnection = Instantiate(targetConnectionPrefab,new Vector2(10000, 10000), Quaternion.identity).GetComponent<TargetConnectionUI>();
        currentConnection.GetComponent<TargetConnectionUI>().startPoint = start;
        currentConnection.GetComponent<TargetConnectionUI>().endPoint = end;
        connections.Add(currentConnection);
    }

    internal List<GameObject> GetTargetList()
    {
        List<GameObject> targets = new List<GameObject>();
        foreach(GameObject go in targetList.Keys)
        {
            targets.Add(go);
        }
        return targets;
    }
}
