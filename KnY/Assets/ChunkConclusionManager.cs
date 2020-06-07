using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChunkConclusionManager : MonoBehaviour
{
    public List<Interactable> interactables;
    private ChunkSettings cs;
    private SpawnDirector_Room spawnDirector;
    public bool interactableConditionsMet = false;
    public bool enemyConditonsMet = false;
    // Start is called before the first frame update
    void Start()
    {
        cs = GetComponent<ChunkSettings>();
        spawnDirector = GetComponent<SpawnDirector_Room>();
    }

    void Update()
    {
        interactableConditionsMet = true;
        foreach (Interactable interactable in interactables)
        {
            interactableConditionsMet = false;
            if (interactable.successfullInteraction)
            {
                interactableConditionsMet = true;
                break;
            }
        }
        enemyConditonsMet = true;
        foreach (GameObject g in spawnDirector.enemyList)
        {
            if (g != null)
            {
                if(g.GetComponent<Statusmanager>().Hp> 0)
                {
                    enemyConditonsMet = false;
                }
            }
        }
        if(enemyConditonsMet == true && interactableConditionsMet == true && GetComponent<Chunk_TriggerEvent>().isTriggerd)
        {
            cs.Concluded = true;
            this.enabled = false;
        }
    }
}
