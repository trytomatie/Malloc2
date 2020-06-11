using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = Director.GetInstance().timePassed.Minutes.ToString("D2") + ":" + Director.GetInstance().timePassed.Seconds.ToString("D2") + ":" + (Director.GetInstance().timePassed.Milliseconds / 10).ToString("D2");
    }
}
