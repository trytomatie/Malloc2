using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnDirector_Room : MonoBehaviour
{
    public List<Interactable> interactablePool;
    public List<MobSpawnAtributes_ScriptableObject> mobPool;
    public List<Transform> spawnLocations;
    public List<Transform> interactableSpawnLocations;
    public DateTime lastSpawnTime = DateTime.Now;
    private System.Random rnd;
    private float timer = 0;
    public MapGenerator mapGenerator;

    public List<GameObject> enemyList = new List<GameObject>();
    public bool hasBeenTriggerd = false;
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
    }

    public void SpawnInteractables()
    {
     
    }

    public void SpawnEnemys()
    {
        hasBeenTriggerd = true;
        foreach (Transform spawnLocation in spawnLocations)
        {
            SpawnRandomMob(spawnLocation);
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
        int i = rnd.Next(0, mobPool.Count);
        mob = mobPool[i];
        int level = 1;
        int exploredChunks = mapGenerator.ExploredChunks.Count;
        level = Mathf.CeilToInt(exploredChunks/3);
        Transform location = spawnLocation;
        GameObject monster = Instantiate(mob.instance, location.transform.position, Quaternion.identity);
        monster.GetComponent<Statusmanager>().level = level;
        enemyList.Add(monster);
    }
    public void SpawnRandomMob(Transform spawnLocation,GameObject target)
    {
        MobSpawnAtributes_ScriptableObject mob = null;
        int i = rnd.Next(0, mobPool.Count);
        mob = mobPool[i];
        int level = 1;
        int exploredChunks = mapGenerator.ExploredChunks.Count;
        level = Mathf.CeilToInt(exploredChunks / 3);
        Transform location = spawnLocation;
        GameObject monster = Instantiate(mob.instance, location.transform.position, Quaternion.identity);
        monster.GetComponent<Statusmanager>().level = level;
        monster.GetComponent<BaseEnemyAI>().Target = target;
        monster.GetComponent<BaseEnemyAI>().mode = BaseEnemyAI.Mode.Ftarget_RegularFollow;
        enemyList.Add(monster);
    }
}
