using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Chunk_TriggerEvent : MonoBehaviour
{
    public UnityEvent onTriggerAction;
    private SpawnDirector_Room mySpawnDirector;
    public GameObject barrier;
    public bool isTriggerd = false;
    public GameObject goThatTriggeredMe;

    public SpawnDirector_Room MySpawnDirector
    {
        get
        {
            if(mySpawnDirector == null)
            {
                mySpawnDirector = GetComponent<SpawnDirector_Room>();
            }
            return mySpawnDirector;
        }

        set
        {
            mySpawnDirector = value;
        }
    }

    private void Start()
    {
        DisableBarrier();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null && isTriggerd == false)
        {
            goThatTriggeredMe = collision.gameObject;
            onTriggerAction.Invoke();
            isTriggerd = true;
        }

    }

    public void SpawnMobs()
    {
        MySpawnDirector.SpawnEnemys(goThatTriggeredMe);
    }

    public void SpawnInteractables()
    {
        MySpawnDirector.SpawnInteractables();
    }

    public void EnableBarrier()
    {
        Material m = barrier.GetComponent<TilemapRenderer>().material;
        Director.GetInstance().SetFadeMaterial(0, 1, m, 2f);
        barrier.GetComponent<TilemapCollider2D>().enabled = true;
    }

    public void DisableBarrier()
    {
        Material m = barrier.GetComponent<TilemapRenderer>().material;
        Director.GetInstance().SetFadeMaterial(1, 0, m, 2f);
        barrier.GetComponent<TilemapCollider2D>().enabled = false;
    }

    public void NormalEnemyRoomEvent()
    {
        EnableBarrier();
        SpawnMobs();


    }
}
