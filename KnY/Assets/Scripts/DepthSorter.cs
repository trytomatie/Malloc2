using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthSorter : MonoBehaviour {
    public double offset = 0;
	// Update is called once per frame
	void Update () {
        GetComponent<SpriteRenderer>().sortingOrder = (int)((transform.position.y + offset) * -10) ;
	}
}
