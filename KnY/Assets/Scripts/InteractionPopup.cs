using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPopup : MonoBehaviour
{
    public Transform _target;
    public Vector3 offset;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            transform.position = new Vector3(10000, 10000, 100);
        }
        else
        {
            if(GameObject.Find("Canvas").GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceCamera)
            {
                transform.position = _target.transform.position + offset;
            }
            else
            { 
            transform.position = Camera.main.WorldToScreenPoint(_target.transform.position + offset);
            }
        }
    }
}
