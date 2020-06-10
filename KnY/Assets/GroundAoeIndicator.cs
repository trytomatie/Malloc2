using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GroundAoeIndicator : MonoBehaviour
{
    float duration = 0;
    float endDuration = 0;


    private void Update()
    {
        if(duration < endDuration)
        {
            duration += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, duration / endDuration);
        }
        if(duration > endDuration)
        {
            Destroy(gameObject);
        }
    }


    public static void InstantiateGroundAoeIndicator(Vector2 position, Vector2 size,float duration)
    {
        GameObject indicator = Instantiate(Director.GetInstance().groundAoeIndicator, position, Quaternion.identity);
        indicator.GetComponent<GroundAoeIndicator>().endDuration = duration;

    }
}
