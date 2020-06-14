using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnDirector_Room : MonoBehaviour
{
    public List<ScriptableObject_InteractableSpawnCard> interactablePool;
    public List<MobSpawnAtributes_ScriptableObject> mobPool;
    private List<Transform> spawnLocations = new List<Transform>();
    private List<Transform> interactableSpawnLocations = new List<Transform>();
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

    public List<Transform> SpawnLocations
    {
        get
        {
            if(spawnLocations.Count == 0)
            {
                Transform parent = transform.Find("MobSpawnPoints");
                for(int i = 0; i < parent.childCount;i++)
                {
                    spawnLocations.Add(parent.GetChild(i));
                }
            }
            return spawnLocations;
        }

        set
        {
            spawnLocations = value;
        }
    }

    public List<Transform> InteractableSpawnLocations
    {
        get
        {
            if (interactableSpawnLocations.Count == 0)
            {
                Transform parent = transform.Find("InteractableSpawnPoints");
                for (int i = 0; i < parent.childCount; i++)
                {
                    interactableSpawnLocations.Add(parent.GetChild(i));
                }
            }
            return interactableSpawnLocations;
        }

        set
        {
            interactableSpawnLocations = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rnd = GetComponent<ChunkSettings>().myRandom;
        if(mapGenerator == null)
        {
            mapGenerator = FindObjectOfType<MapGenerator>();
        }
        if(SpawnLocations.Count == 0)
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
        interactableSpawned.GetComponent<Interactable_BlankChest>().cost = interactable.cost;
    }

    public void SpawnEnemys()
    {
        hasBeenTriggerd = true;
        foreach (Transform spawnLocation in SpawnLocations)
        {
            SpawnRandomMob(spawnLocation);
        }
    }

    public void SpawnInteractables()
    {
        foreach (Transform spawnLocation in InteractableSpawnLocations)
        {
            SpawnRandomInteractable(spawnLocation);
        }
    }

    public void SpawnEnemys(GameObject target)
    {
        hasBeenTriggerd = true;
        foreach (Transform spawnLocation in SpawnLocations)
        {
            SpawnRandomMob(spawnLocation,target);
        }
    }

    public void SpawnRandomMob(Transform spawnLocation)
    {
        MobSpawnAtributes_ScriptableObject mob = null;
        int rndChance = rnd.Next(0, CombinedWeight);
        List<MobSpawnAtributes_ScriptableObject> myMobPool = mobPool;
        if(spawnLocation.GetComponent<ForcedSpawn>() != null && spawnLocation.GetComponent<ForcedSpawn>().mobPool.Count > 0)
        {
            myMobPool = spawnLocation.GetComponent<ForcedSpawn>().mobPool;
        }
        foreach (MobSpawnAtributes_ScriptableObject m in myMobPool)
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
        monster.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_KingsContract((int)Director.GetInstance().timePassed.TotalSeconds));
        enemyList.Add(monster);
    }
    public void SpawnRandomMob(Transform spawnLocation,GameObject target)
    {
        MobSpawnAtributes_ScriptableObject mob = null;
        List<MobSpawnAtributes_ScriptableObject> myMobPool = mobPool;
        int myCombinedWeight = CombinedWeight;
        if (spawnLocation.GetComponent<ForcedSpawn>() != null && spawnLocation.GetComponent<ForcedSpawn>().mobPool.Count > 0)
        {
            myMobPool = spawnLocation.GetComponent<ForcedSpawn>().mobPool;
            myCombinedWeight = 0;
            foreach (MobSpawnAtributes_ScriptableObject m in spawnLocation.GetComponent<ForcedSpawn>().mobPool)
            {
                myCombinedWeight += m.chanceToSpawn;
            }
        }
        int rndChance = rnd.Next(0, myCombinedWeight);
        foreach (MobSpawnAtributes_ScriptableObject m in myMobPool)
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
        monster.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_KingsContract((int)Director.GetInstance().timePassed.TotalSeconds));
        monster.GetComponent<AI_BaseAI>().Target = target;
        monster.GetComponent<AI_BaseAI>().mode = AI_BaseAI.Mode.Ftarget_RegularFollow;
        enemyList.Add(monster);
    }
}
