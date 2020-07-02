using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Component : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Director.GetInstance().isMobile)
        {
            gameObject.SetActive(false);
        }
    }
}
