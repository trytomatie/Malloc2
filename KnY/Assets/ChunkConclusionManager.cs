using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChunkConclusionManager : MonoBehaviour
{
    public List<Interactable> interactables;
    private ChunkSettings cs;
    // Start is called before the first frame update
    void Start()
    {
        cs = GetComponent<ChunkSettings>();
    }

    void Update()
    {
        foreach(Interactable interactable in interactables)
        {
            if (interactable.successfullInteraction)
            {
                cs.concluded = true;
            }
        }

    }
}
