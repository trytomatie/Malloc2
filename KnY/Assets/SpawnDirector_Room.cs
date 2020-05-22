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

    private List<GameObject> enemyList = new List<GameObject>();
    private bool roomCleared = false;
    private bool firstInstantiation = true;
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
    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < 1)
        {
            timer += Time.fixedDeltaTime;
        }

        if (timer >= 1)
        {
            TimeSpan timeSinceLastSpawn = DateTime.Now - lastSpawnTime;
            if (firstInstantiation)
            {
                SpawnInteractables();
                SpawnEnemys();
                firstInstantiation = false;
            }
            if(roomCleared == true && timeSinceLastSpawn.TotalSeconds > 120) 
            {
                lastSpawnTime = DateTime.Now;
                SpawnEnemys();
                roomCleared = false;
            }
            timer = 0;
        }
    }

    public void SpawnInteractables()
    {
     
    }

    public void SpawnEnemys()
    {
        foreach(Transform spawnLocation in spawnLocations)
        {
            SpawnRandomMob(spawnLocation);
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

    }
}
