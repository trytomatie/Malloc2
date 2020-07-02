using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Chunk_TriggerEvent : MonoBehaviour
{
    public UnityEvent onTriggerAction;
    private SpawnDirector_Room mySpawnDirector;
    public List<ScriptableObject_InteractableSpawnCard> price = new List<ScriptableObject_InteractableSpawnCard>();
    public GameObject barrier;
    public bool isTriggerd = false;
    public GameObject goThatTriggeredMe;
    private int initialLevelOfGoThatTriggeredMe = 0;
    private ChunkSettings myChunkSettings;

    private readonly float CAMERABORDER_X = 2.48f;

    private readonly float CAMERABORDER_Y = 1.68f;

    private void Start()
    {
        myChunkSettings = GetComponent<ChunkSettings>();
        if (price.Count == 0)
        {
            price = Director.MapGenerator.manaCrystals;
        }
        DisableBarrier();
    }


    /// <summary>
    /// Checks the trigger of the room
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null && isTriggerd == false)
        {
            isTriggerd = true;
            goThatTriggeredMe = collision.gameObject;
            initialLevelOfGoThatTriggeredMe = collision.gameObject.GetComponent<Statusmanager>().level;
            onTriggerAction.Invoke();
        }

    }


    /// <summary>
    /// Spawns mobs from spawndirector
    /// </summary>
    public void SpawnMobs()
    {
        MySpawnDirector.SpawnEnemys(goThatTriggeredMe);
    }


    /// <summary>
    /// Spawns Interactables from spawndirector
    /// </summary>
    public void SpawnInteractables()
    {
        MySpawnDirector.SpawnInteractables();
    }


    /// <summary>
    /// Enables the Barrier
    /// </summary>
    public void EnableBarrier()
    {
        Material m = barrier.GetComponent<TilemapRenderer>().material;
        Director.GetInstance().SetFadeMaterial(0, 1, m, 2f);
        barrier.GetComponent<TilemapCollider2D>().enabled = true;
    }


    /// <summary>
    /// Disables the barrier
    /// </summary>
    public void DisableBarrier()
    {
        Material m = barrier.GetComponent<TilemapRenderer>().material;
        Director.GetInstance().SetFadeMaterial(1, 0, m, 2f);
        barrier.GetComponent<TilemapCollider2D>().enabled = false;
    }


    /// <summary>
    /// Disables Barrier and spawns price (If fertile)
    /// </summary>
    public void DisableBarrierAndSpawnInteractable()
    {
        if(myChunkSettings.isFertile || initialLevelOfGoThatTriggeredMe != goThatTriggeredMe.GetComponent<Statusmanager>().level)
        { 
            ScriptableObject_InteractableSpawnCard targetPrice = null;
            float rndChance = (float)new System.Random().NextDouble() * (CombinedPriceWeight);
            foreach (ScriptableObject_InteractableSpawnCard m in price)
            {
                if (m.chanceToSpawn >= rndChance)
                {
                    targetPrice = m;
                    break;
                }
                rndChance -= m.chanceToSpawn;
            }
            if (targetPrice == null)
            {
                return;
            }
            Instantiate(targetPrice.instance, transform.position, Quaternion.identity);
        }
        DisableBarrier();
    }



    /// <summary>
    /// Spawns the stairs, and the price (If fertile)
    /// </summary>
    public void DisableBarrierAndSpawnInteractableAndSpawnStairs()
    {
        if (myChunkSettings.isFertile && initialLevelOfGoThatTriggeredMe != goThatTriggeredMe.GetComponent<Statusmanager>().level)
        {
            ScriptableObject_InteractableSpawnCard targetPrice = null;
            float rndChance = (float)new System.Random().NextDouble() * (CombinedPriceWeight);
            foreach (ScriptableObject_InteractableSpawnCard m in price)
            {
                if (m.chanceToSpawn >= rndChance)
                {
                    targetPrice = m;
                    break;
                }
                rndChance -= m.chanceToSpawn;
            }
            if (targetPrice == null)
            {
                return;
            }
            Instantiate(targetPrice.instance, transform.position, Quaternion.identity);
        }

        Instantiate(PublicGameResources.GetResource().stairs, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        DisableBarrier();
    }


    /// <summary>
    /// TriggersOnRoomEnterEffects for the go that entered
    /// </summary>
    /// <param name="g"></param>
    public void TrigerOnRoomEnterEffects(GameObject g)
    {
        g.GetComponent<Statusmanager>().TriggerOnRoomEnterEffects();
    }


    /// <summary>
    /// Normal Enemy Room event
    /// </summary>
    public void NormalEnemyRoomEvent()
    {
        EnableBarrier();
        SpawnMobs();
        SummonFollowers();
        TrigerOnRoomEnterEffects(goThatTriggeredMe);
        CameraFollow.SetAndEnableBorders(new Vector2(transform.position.x - CAMERABORDER_X, transform.position.y + CAMERABORDER_Y), new Vector2(transform.position.x + CAMERABORDER_X, transform.position.y - CAMERABORDER_Y));
    }


    /// <summary>
    /// Summons Followers into the triggerd room
    /// </summary>
    public void SummonFollowers()
    {

        bool scoutActive = false;
        if(goThatTriggeredMe.GetComponent<Statusmanager>().ContainsStatusEffect(new StatusEffect_ItemSeriesScout()))
        {
            scoutActive = true;
        }
        foreach(GameObject follower in goThatTriggeredMe.GetComponent<Statusmanager>().Followers)
        {
            if(scoutActive)
            {
                follower.transform.position = GetComponent<SpawnDirector_Room>().SpawnLocations[UnityEngine.Random.Range(0, GetComponent<SpawnDirector_Room>().SpawnLocations.Count)].position;
            }
            else
            { 
                follower.transform.position = (Vector2)goThatTriggeredMe.transform.position + new Vector2(UnityEngine.Random.Range(0.1f, 0.1f), UnityEngine.Random.Range(0.1f, 0.1f));
            }
        }
    }


    #region Properties
    private float CombinedPriceWeight
    {
        get
        {
            float value = 0;
            foreach (ScriptableObject_InteractableSpawnCard card in price)
            {
                value += card.chanceToSpawn;
            }
            return value;
        }
    }


    public SpawnDirector_Room MySpawnDirector
    {
        get
        {
            if (mySpawnDirector == null)
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

    #endregion
}
