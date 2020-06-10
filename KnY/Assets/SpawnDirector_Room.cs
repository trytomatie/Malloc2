using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnDirector_Room : MonoBehaviour
{
    public List<ScriptableObject_InteractableSpawnCard> interactablePool;
    public List<MobSpawnAtributes_ScriptableObject> mobPool;
    public List<Transform> spawnLocations;
    public List<Transform> interactableSpawnLocations;
    private System.Random rnd;
    private float timer = 0;
    public MapGenerator mapGenerator;
    public static DateTime startTime = DateTime.Now;

    private int combinedWeight = 0;
    private float combinedInteractableWeight = 0;

    public List<GameObject> enemyList = new List<GameObject>();
    public bool hasBeenTriggerd = false;

    public int CombinedWeight
    {
        get
        {
            if(combinedWeight == 0)
            {
                foreach(MobSpawnAtributes_ScriptableObject mob in mobPool)
                {
                    combinedWeight+= mob.chanceToSpawn;
                }
            }
            return combinedWeight;
        }

        set
        {
            combinedWeight = value;
        }
    }

    public float CombinedInteractableWeight
    {
        get
        {
            if (combinedInteractableWeight == 0)
            {
                foreach (ScriptableObject_InteractableSpawnCard interactable in interactablePool)
                {
                    combinedInteractableWeight += interactable.chanceToSpawn;
                }
            }
            return combinedInteractableWeight;
        }

        set
        {
            combinedInteractableWeight = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rnd = Director.GetInstance().globalRandom;
        if(mapGenerator == null)
        {
            mapGenerator = FindObjectOfType<MapGenerator>();
        }
        if(spawnLocations.Count == 0)
        {
            Debug.LogWarning("No MobSpawnLocations Defined!");
        }
        SpawnInteractables();
        
    }

    public void SpawnRandomInteractable(Transform spawnLocation)
    {
        ScriptableObject_InteractableSpawnCard interactable = null;
        float rndChance = (float)rnd.NextDouble() * (CombinedInteractableWeight - 0)+0;
        foreach (ScriptableObject_InteractableSpawnCard m in interactablePool)
        {
            if (m.chanceToSpawn >= rndChance)
            {
                interactable = m;
                break;
            }
            rndChance -= m.chanceToSpawn;
        }
        if(interactable == null)
        {
            return;
        }
        GameObject interactableSpawned = Instantiate(interactable.instance, spawnLocation.transform.position, Quaternion.identity);
        interactableSpawned.GetComponent<Interactable_Chest>().cost = interactable.cost;
    }

    public void SpawnEnemys()
    {
        hasBeenTriggerd = true;
        foreach (Transform spawnLocation in spawnLocations)
        {
            SpawnRandomMob(spawnLocation);
        }
    }

    public void SpawnInteractables()
    {
        foreach (Transform spawnLocation in interactableSpawnLocations)
        {
            SpawnRandomInteractable(spawnLocation);
        }
    }

    public void SpawnEnemys(GameObject target)
    {
        hasBeenTriggerd = true;
        foreach (Transform spawnLocation in spawnLocations)
        {
            SpawnRandomMob(spawnLocation,target);
        }
    }

    public void SpawnRandomMob(Transform spawnLocation)
    {
        MobSpawnAtributes_ScriptableObject mob = null;
        int rndChance = rnd.Next(0, CombinedWeight);
        foreach (MobSpawnAtributes_ScriptableObject m in mobPool)
        {
            if (m.chanceToSpawn >= rndChance)
            {
                mob = m;
                break;
            }
            rndChance -= m.chanceToSpawn;
        }
        int level = 1;
        level = mapGenerator.currentFloor;
        Transform location = spawnLocation;
        GameObject monster = Instantiate(mob.instance, location.transform.position, Quaternion.identity);
        monster.GetComponent<Statusmanager>().level = level;
        TimeSpan timeSpan = DateTime.Now - startTime;
        monster.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_KingsContract((int)timeSpan.TotalSeconds));
        enemyList.Add(monster);
    }
    public void SpawnRandomMob(Transform spawnLocation,GameObject target)
    {
        MobSpawnAtributes_ScriptableObject mob = null;
        int rndChance = rnd.Next(0, CombinedWeight);
        foreach (MobSpawnAtributes_ScriptableObject m in mobPool)
        { 
            if (m.chanceToSpawn >= rndChance)
            {
                mob = m;
                break;
            }
            rndChance -= m.chanceToSpawn;
        }
        int level = 1;
        level = mapGenerator.currentFloor;
        Transform location = spawnLocation;
        GameObject monster = Instantiate(mob.instance, location.transform.position, Quaternion.identity);
        monster.GetComponent<Statusmanager>().level = level;
        TimeSpan timeSpan = DateTime.Now - startTime;
        monster.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_KingsContract((int)timeSpan.TotalSeconds));
        monster.GetComponent<BaseEnemyAI>().Target = target;
        monster.GetComponent<BaseEnemyAI>().mode = BaseEnemyAI.Mode.Ftarget_RegularFollow;
        enemyList.Add(monster);
    }
}
