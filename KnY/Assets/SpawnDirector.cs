using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnDirector : MonoBehaviour
{

    public List<Transform> mobSpawnLocations;
    public List<Transform> interactableSpawnLocations;

    public List<Interactable> interactablePool;
    public List<MobSpawnAtributes_ScriptableObject> mobPool;
    public List<int> spawnCredditsPerWave;
    public List<int> interactablesPerWave;

    public int wave = 0;
    public double baseCreditsPerTick = 2;
    public double creditsPerTick = 2.5; // per wave
    public DateTime lastSpawnTime = DateTime.Now;

    public double spawnCredits = 0;
    public double spawnCreditsEarnedThisWave = 0;
    private System.Random rnd;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rnd = Director.GetInstance().globalRandom;
    }

    void Update()
    {
        //temp
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (wave == 0)
            {
                wave = 1;
                GameObject.Find("RoomText").GetComponent<Text>().text = "Wave " + wave;
                spawnCredits = 0;
                spawnCreditsEarnedThisWave = 0;
                SpawnInteractables();
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {



        if(wave == 0)
        {
            return;
        }

        if(spawnCredditsPerWave[wave] <= spawnCreditsEarnedThisWave)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                
                wave++;
                spawnCreditsEarnedThisWave = 0;
                SpawnInteractables();
                GameObject.Find("RoomText").GetComponent<Text>().text = "Wave " + wave;
            }
            return;
        }

        if(timer < 1)
        {
            timer += Time.fixedDeltaTime;
        }

        if(timer >= 1)
        {
            double creditsToEarn = baseCreditsPerTick + creditsPerTick * wave;
            spawnCredits += creditsToEarn;
            spawnCreditsEarnedThisWave += creditsToEarn;
            TimeSpan timeSinceLastSpawn = DateTime.Now - lastSpawnTime;
            int rndVal = rnd.Next(0, 36);
            if(rndVal < timeSinceLastSpawn.TotalSeconds)
            {
                SpendCredits();
                lastSpawnTime = DateTime.Now;
            }
            timer = 0;
        }

    }

    private void SpendCredits()
    {
        int timeOut = 0;
        double creditsToSpend = rnd.Next((int)(spawnCredits * 0.6f), (int)spawnCredits);
        int numberOfBatches = rnd.Next(1, (int)Mathf.Clamp((float)creditsToSpend, 1, 4));
        double halfBatchValues = (double)creditsToSpend / (double)(numberOfBatches * 2);
        int numberOfHalfBatchValuesLeft = numberOfBatches * 2;
        double[] batchValue = new double[numberOfBatches];
        while(numberOfHalfBatchValuesLeft>0 && timeOut < 1000)
        { 
            for(int i = 0;i < batchValue.Length;i++)
            {
                int halfBatches = rnd.Next(0, numberOfHalfBatchValuesLeft+1);
                int totalValue = (int)Math.Floor(halfBatches * halfBatchValues);
                batchValue[i] = totalValue;
                numberOfHalfBatchValuesLeft -= halfBatches;
            }
            timeOut++;
        }
        for (int i = 0; i < batchValue.Length; i++)
        {
            while(batchValue[i] > 0 && timeOut < 1000)
            {
                double creditsSpent = SpawnRandomMob(batchValue[i], wave);
                batchValue[i] -= creditsSpent;
                spawnCredits -= creditsSpent;
                timeOut++;
            }
        }

        if(timeOut > 1000)
        {
            print("SpawnDirector timedOut!");
        }
    }

    public double SpawnRandomMob(double creditsAvailable,int waveCount)
    {
        MobSpawnAtributes_ScriptableObject mob = null;
        int timeOut = 0;
        while(mob == null && timeOut < 100)
        {
            int i = rnd.Next(0,mobPool.Count);
            if (mobPool[i].baseCost <= creditsAvailable)
            {
                mob = mobPool[i];
                break;
            }
            timeOut++;
        }

        if(mob == null)
        {
            return creditsAvailable;
        }

        timeOut = 0;
        double cost = creditsAvailable +1;
        int level = 1;
        while (cost > creditsAvailable && timeOut < 100 )
        { 
            level = rnd.Next(Mathf.Clamp(waveCount-4,0,waveCount), waveCount+1);
            cost = mob.baseCost + mob.costPerLevel * level;
            timeOut++;
        }
        print(timeOut);
        if(cost <= creditsAvailable)
        {
            Transform location = mobSpawnLocations[rnd.Next(0, mobSpawnLocations.Count)];
            GameObject monster = Instantiate(mob.instance, location.transform.position, Quaternion.identity);
            monster.GetComponent<Statusmanager>().level = level * 3;
            return cost;
        }
        return creditsAvailable;

    }

    public void SpawnInteractables()
    {
        RemoveInteractables();
        int spawns = interactablesPerWave[wave];
        List<Transform> spawnLocationList = new List<Transform>();
        foreach(Transform t in interactableSpawnLocations)
        {
            spawnLocationList.Add(t);
        }
        int spawnsLeft = interactablesPerWave[wave];
        while(spawnLocationList.Count > 0 && spawnsLeft > 0)
        {
            Transform spawnLocation = spawnLocationList[rnd.Next(0, spawnLocationList.Count)];
            Interactable g = Instantiate(interactablePool[rnd.Next(0, interactablePool.Count)], spawnLocation.transform.position, Quaternion.identity);
            if(g.GetComponent<Interactable_Chest>() != null)
            {
                g.GetComponent<Interactable_Chest>().cost *= wave;
            }
            spawnLocationList.Remove(spawnLocation);
            spawnsLeft--;
            
        }
        
    }

    public void RemoveInteractables()
    {
        GameObject[] interactablesToBeRemoved = GameObject.FindGameObjectsWithTag("SpawnedInteractable");
        foreach(GameObject go in interactablesToBeRemoved)
        {
            Destroy(go);
        }
    }
}
