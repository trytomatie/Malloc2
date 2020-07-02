using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UI_MobileComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!Director.GetInstance().isMobile)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
