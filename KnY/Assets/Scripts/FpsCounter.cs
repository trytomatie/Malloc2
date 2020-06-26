using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    private float timer;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0.5f)
        { 
        GetComponent<Text>().text = "Fps: " + (int)(1f / Time.unscaledDeltaTime);
            timer -= 0.5f;
        }
        timer += Time.unscaledDeltaTime;
    }

}
