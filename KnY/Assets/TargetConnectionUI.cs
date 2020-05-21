using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetConnectionUI : MonoBehaviour {

    public Transform startPoint;
    public Transform endPoint;

    private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update (){
        Vector2 middlePoint = (startPoint.position + endPoint.position) / 2;
        float distance = Vector2.Distance(startPoint.position, endPoint.position);
        transform.position = middlePoint;
        transform.right = endPoint.position - startPoint.position;
        spriteRenderer.size = new Vector2(distance, 0.32f);

    }
}
