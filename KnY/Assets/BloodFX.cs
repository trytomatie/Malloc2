using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFX : MonoBehaviour {
    public Sprite[] bloodSprite;
	// Use this for initialization
	void Start () {
        SetBloodSprite(Random.Range(0, bloodSprite.Length));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetBloodSprite(int i)
    {
        GetComponent<SpriteRenderer>().sprite = bloodSprite[i];
    }
}
