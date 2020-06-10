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
    public GameObject trader;
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
    bool bossSpawned = false;
    bool prepPhase = true;
    private DateTime startTime;
    // Start is called before the first frame update
    void Start()
    {
        rnd = Director.GetInstance().globalRandom;
        startTime = DateTime.Now;
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
                prepPhase = false;
                spawnCreditsEarnedThisWave = 0;
                SpawnInteractables();
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && prepPhase == true)
                {
                    wave++;
                    spawnCreditsEarnedThisWave = 0;
                    prepPhase = false;
                    SpawnInteractables();
                    GameObject.Find("RoomText").GetComponent<Text>().text = "Wave " + wave;
                }
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
        if (spawnCredditsPerWave[wave] <= spawnCreditsEarnedThisWave && bossSpawned == false && wave %3 == 0 && prepPhase == false && GameObject.FindGameObjectsWithTag("Enemy").Length < 10) // TEMP Boss spawning
        {
            UI_InfoTitleManager.Show("A terrible monster has appeared!", "", 3f);
            SpawnSpecificMob(mobPool[2], wave);
            bossSpawned = true;
        }
        if(spawnCredits <= 0 && spawnCredditsPerWave[wave] <= spawnCreditsEarnedThisWave)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && prepPhase == false)
            {
                prepPhase = true;
                GameObject.Find("RoomText").GetComponent<Text>().text = "Preperation Phase " + wave;
                Instantiate(trader, new Vector3(0, 1, 0), Quaternion.identity);
                Instantiate(trader, new Vector3(0.5f, 1, 0), Quaternion.identity);
                Instantiate(trader, new Vector3(-0.5f, 1, 0), Quaternion.identity);
                bossSpawned = false;
            }
        }

        if(timer < 1)
        {
            timer += Time.fixedDeltaTime;
        }

        if(timer >= 1)
        {
            if (spawnCredditsPerWave[wave] > spawnCreditsEarnedThisWave)
            {
                double creditsToEarn = baseCreditsPerTick + creditsPerTick * wave;
                spawnCredits += creditsToEarn;
                spawnCreditsEarnedThisWave += creditsToEarn;
            }
            TimeSpan timeSinceLastSpawn = DateTime.Now - lastSpawnTime;
            int rndVal = rnd.Next(0, 32);
            if(rndVal < timeSinceLastSpawn.TotalSeconds || GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                SpendCredits();
                lastSpawnTime = DateTime.Now;
            }
            timer = 0;
        }

    }

    private void SpendCredits()
    {
        if(spawnCredits < 0)
        {
            return;
        }
        int timeOut = 0;
        double creditsToSpend = rnd.Next((int)(spawnCredits * 0.6f), (int)spawnCredits);
        if(((float)spawnCredits / (float)spawnCreditsEarnedThisWave) < 0.2f)
        {
            creditsToSpend = spawnCredits;
        }
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
            int rndChance = rnd.Next(0, 100);
            if (mobPool[i].baseCost <= creditsAvailable && mobPool[i].chanceToSpawn >= rndChance)
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
            level = rnd.Next(Mathf.Clamp(waveCount-2,0,waveCount), waveCount+1);
            cost = mob.baseCost + mob.costPerLevel * level;
            timeOut++;
        }
        if(cost <= creditsAvailable)
        {
            Transform location = mobSpawnLocations[rnd.Next(0, mobSpawnLocations.Count)];
            GameObject monster = Instantiate(mob.instance, location.transform.position + new Vector3(UnityEngine.Random.Range(-0.1f,0.1f), UnityEngine.Random.Range(-0.1f, 0.1f),0), Quaternion.identity);
            monster.GetComponent<Statusmanager>().level = level;
            monster.GetComponent<BaseEnemyAI>().aggroRadius = 10.52f;
            if (wave > 4)
            {
                TimeSpan timeSpan = DateTime.Now- startTime;
                monster.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_KingsContract((int)timeSpan.TotalSeconds));
            }
            return cost;
        }
        return creditsAvailable;

    }

    public void SpawnSpecificMob(MobSpawnAtributes_ScriptableObject mob,int waveCount)
    {
        int level = 1;
        level = waveCount;
        Transform location = mobSpawnLocations[rnd.Next(0, mobSpawnLocations.Count)];
        GameObject monster = Instantiate(mob.instance, location.transform.position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 0), Quaternion.identity);
        monster.GetComponent<Statusmanager>().level = level;
        if (wave > 7)
        {
            TimeSpan timeSpan = DateTime.Now - startTime;
            monster.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_KingsContract((int)timeSpan.TotalSeconds));
        }
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
                g.GetComponent<Interactable_Chest>().cost = 12 +( 30 * (wave-1));
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
