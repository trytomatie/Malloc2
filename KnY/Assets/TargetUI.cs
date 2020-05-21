using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUI : MonoBehaviour {
    public Sprite[] targetSprite;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTargetSprite(int i)
    {
        if(i > 6)
        {
            i = 6;
        }
        GetComponent<SpriteRenderer>().sprite = targetSprite[i-1];
    }
}
