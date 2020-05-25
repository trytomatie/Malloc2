using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public Transform mapParent;
    public List<GameObject> chunkTiles = new List<GameObject>();
    public List<float> likelyhoodTable = new List<float>();
    [ReadOnly]
    private int combinedLikelyhood;


    public System.Random seed = new System.Random(1337);
    private System.Random xSeed;
    private System.Random ySeed;
    private System.Random negativeXSeed;
    private System.Random negativeYSeed;

    private List<int> xSeedMap = new List<int>();
    private List<int> ySeedMap = new List<int>();
    private List<int> negativeXSeedMap = new List<int>();
    private List<int> negativeYSeedMap = new List<int>();

    public Dictionary<Vector2, GameObject> chunkMap = new Dictionary<Vector2, GameObject>(); // Chunks that are generated
    public Dictionary<Vector2, GameObject> exploredChunks = new Dictionary<Vector2, GameObject>(); // The Chunks the player Has visited physicaly
    public Dictionary<Vector2, bool> tunnelMap = new Dictionary<Vector2, bool>(); // Map of the tunnels where chunks are supposed to be able to spawn
    public Vector2 currentCameraCoords = new Vector2();
    private float CHUNKSIZE = 4.8f;

    public Vector2 CurrentCameraCoords
    {
        get
        {
            return currentCameraCoords;
        }

        set
        {
            if(value != currentCameraCoords)
            {
                StartCoroutine(LoadingAndUnloading(currentCameraCoords, value));
                currentCameraCoords = value;
            }
        }
    }

    public Dictionary<Vector2, GameObject> ExploredChunks
    {
        get
        {
            return exploredChunks;
        }

        set
        {
            exploredChunks = value;
        }
    }

    IEnumerator LoadingAndUnloading(Vector2 toUnload,Vector2 toLoad)
    {
        if(!ExploredChunks.ContainsKey(toUnload))
        {
            ExploredChunks.Add(toUnload,null);
            GameObject.Find("RoomText").GetComponent<Text>().text = "Rooms Explored: " + ExploredChunks.Count;
        }
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
                if (Vector2.Distance(toLoad,toUnload + new Vector2(x,y)) > 1.25f)
                {
                    UnloadChunk(toUnload + new Vector2(x, y));
                } 
            }
        }
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
                if (Mathf.Abs(x) == Mathf.Abs(y) && x != 0)
                {
                    continue;
                }
                if(tunnelMap.ContainsKey(toLoad+new Vector2(x,y)))
                {
                    print("Chunk loaded");
                    LoadChunk(toLoad + new Vector2(x, y), GetSeededChunkId(toLoad + new Vector2(x, y)) );
                }
            }
        }
        AstarPath.active.data.gridGraph.center = currentCameraCoords * CHUNKSIZE;
        AstarPath.active.Scan();
    }
    
    public void UnloadChunk(Vector2 coords)
    {
        if (chunkMap.ContainsKey(coords))
        {
            chunkMap[coords].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        tunnelMap = CreateCollisonMap(25, 25,3);
        SetSeeds();
        foreach(int l in likelyhoodTable)
        {
            combinedLikelyhood += l;
        }
        int x = (int)Mathf.Round((Camera.main.transform.position.x / CHUNKSIZE));
        int y = (int)Mathf.Round((Camera.main.transform.position.y / CHUNKSIZE));
        CurrentCameraCoords = new Vector2(x, y);
        GetClosestChunk(4, 2,0);
    }

    void SetSeeds()
    {
        xSeed = new System.Random(seed.Next());
        negativeXSeed = new System.Random(seed.Next());
        ySeed = new System.Random(seed.Next());
        negativeYSeed = new System.Random(seed.Next());
    }

    // Update is called once per frame
    void Update()
    {
        int x = (int) Mathf.Round((Camera.main.transform.position.x / CHUNKSIZE));
        int y = (int)Mathf.Round((Camera.main.transform.position.y / CHUNKSIZE));
        CurrentCameraCoords = new Vector2(x, y);
    }

    private void LoadChunk(Vector2 coords, int id)
    {
        if(!chunkMap.ContainsKey(coords))
        {
            GenerateNewChunk(coords, id);
        }
        else
        {
            chunkMap[coords].SetActive(true);
        }
    }

    private void GenerateNewChunk(Vector2 coords, int id)
    {
        chunkMap.Add(coords, Instantiate(chunkTiles[id], coords * CHUNKSIZE, Quaternion.identity, mapParent));
    }

    public int GetSeededChunkId(Vector2 coords)
    {
        CheckSeedMap(coords);
        int x = 0;
        int y = 0;
        if (coords.x >= 0)
        {
            x = xSeedMap[(int)coords.x];
        }
        else
        {
            x = negativeXSeedMap[(int)Mathf.Abs(coords.x) - 1];
        }
        if (coords.y >= 0)
        {
            y = ySeedMap[(int)coords.y];
        }
        else
        {
            y = negativeYSeedMap[(int)Mathf.Abs(coords.y) - 1];
        }
        bool roomIsUp = false;
        bool roomIsDown = false;
        bool roomIsRight = false;
        bool roomIsLeft = false;
        GetRoomOpeningsNeeded(coords, ref roomIsUp, ref roomIsDown, ref roomIsRight, ref roomIsLeft);

        int chunkId = -1; // new System.Random(x + y).Next(0, chunkTiles.Count);
        int likelyHood = new System.Random(x - y).Next(0, combinedLikelyhood + 1);
        int i = 0;
        int likelyhoodMemory = 0;
        float combinedChanceOfAllCompatibleChunks = 0; 
        List<int> compatibleChunks = new List<int>();
        for (int o = 0; o < chunkTiles.Count; o++)
        {
            if (chunkTiles[o].GetComponent<ChunkSettings>().CheckOpeningsAvaiable(roomIsUp, roomIsDown, roomIsRight, roomIsLeft))
            {
                compatibleChunks.Add(o);
                combinedChanceOfAllCompatibleChunks += likelyhoodTable[o];
            }
        }
        foreach (int l in likelyhoodTable)
        {
            if (chunkTiles[i].GetComponent<ChunkSettings>().CheckOpeningsAvaiable(roomIsUp, roomIsDown, roomIsRight, roomIsLeft))
            {
                if (likelyHood + likelyhoodMemory <= l + likelyhoodMemory)
                {
                    chunkId = i;
                    break;
                }
                likelyhoodMemory += l;
            }
            i++;
        }
        if (chunkId == -1)
        {
            chunkId = compatibleChunks[new System.Random(x - y).Next(0, compatibleChunks.Count)];
        }
        return chunkId;
    }

    /// <summary>
    /// Checks all Room cardinals for entries needed
    /// </summary>
    /// <param name="coords"></param>
    /// <param name="roomIsUp"></param>
    /// <param name="roomIsDown"></param>
    /// <param name="roomIsRight"></param>
    /// <param name="roomIsLeft"></param>
    private void GetRoomOpeningsNeeded(Vector2 coords, ref bool roomIsUp, ref bool roomIsDown, ref bool roomIsRight, ref bool roomIsLeft)
    {
        if (tunnelMap.ContainsKey(new Vector2(coords.x + 1, coords.y)))
        {
            roomIsRight = true;
        }

        if (tunnelMap.ContainsKey(new Vector2(coords.x - 1, coords.y)))
        {
            roomIsLeft = true;
        }

        if (tunnelMap.ContainsKey(new Vector2(coords.x, coords.y + 1)))
        {
            roomIsUp = true;
        }

        if (tunnelMap.ContainsKey(new Vector2(coords.x, coords.y - 1)))
        {
            roomIsDown = true;
        }
    }

    public Vector2 GetClosestChunk(int chunkId, int radius,int ignoreRadius,List<Vector2>ignoreList)
    {
        int x = (int)CurrentCameraCoords.x;
        int y = (int)CurrentCameraCoords.y;
        Vector2 foundChunk = Vector2.zero;
        float distanceToClosestChunk = 10000;
        for(int sX = x+ radius; sX > x- radius; sX--)
        {
            for (int sY = y + radius; sY > y - radius; sY--)
            {
                
                Vector2 vector = new Vector2(sX, sY);
                if ((sX < x + ignoreRadius && sX > x - ignoreRadius) && (sY < y + ignoreRadius && sY > y - ignoreRadius)|| ignoreList.Contains(vector))
                {
                    continue;
                }
                if (chunkId == GetSeededChunkId(vector))
                {
                    float distanceToFoundChunk = Vector2.Distance(currentCameraCoords, vector);
                    if (distanceToFoundChunk < distanceToClosestChunk)
                    {
                        distanceToClosestChunk = distanceToFoundChunk;
                        foundChunk = vector;
                    }
                }
            }
        }
        return foundChunk;
    }

    public Dictionary<Vector2,bool> GetAllChunksOfType(int chunkId,int radius,int ignoreRadius,Dictionary<Vector2,bool> existingMap)
    {

        int x = (int)CurrentCameraCoords.x;
        int y = (int)CurrentCameraCoords.y;
        Dictionary<Vector2, bool> foundChunks = new Dictionary<Vector2, bool>();
        for (int sX = x + radius; sX > x - radius; sX--)
        {
            for (int sY = y + radius; sY > y - radius; sY--)
            {
                Vector2 vector = new Vector2(sX, sY);
                if ((sX < x + ignoreRadius && sX > x - ignoreRadius) && (sY < y + ignoreRadius && sY > y - ignoreRadius) || existingMap.ContainsKey(vector))
                {
                    continue;
                }
                if (chunkId == GetSeededChunkId(vector))
                {
                    existingMap.Add(vector, false);
                    foundChunks.Add(vector,false);
                }
            }
        }
        return foundChunks;
    }

    public Dictionary<Vector2, bool> GetAllChunksOfType(List<int> chunkIds, int radius, int ignoreRadius, Dictionary<Vector2, bool> existingMap)
    {

        int x = (int)CurrentCameraCoords.x;
        int y = (int)CurrentCameraCoords.y;
        Dictionary<Vector2, bool> foundChunks = new Dictionary<Vector2, bool>();
        for (int sX = x + radius; sX > x - radius; sX--)
        {
            for (int sY = y + radius; sY > y - radius; sY--)
            {
                Vector2 vector = new Vector2(sX, sY);
                if ((sX < x + ignoreRadius && sX > x - ignoreRadius) && (sY < y + ignoreRadius && sY > y - ignoreRadius) || existingMap.ContainsKey(vector) 
                    || (chunkMap.ContainsKey(vector) && chunkMap[vector].GetComponent<ChunkSettings>() != null && chunkMap[vector].GetComponent<ChunkSettings>().concluded == true))
                {
                    continue;
                }
                foreach(int chunkId in chunkIds)
                { 
                    if (chunkId == GetSeededChunkId(vector))
                    {
                        existingMap.Add(vector, false);
                        foundChunks.Add(vector, false);
                    }
                }
            }
        }
        return foundChunks;
    }
    private Vector2 GetClosestChunk(int chunkId, int radius, int ignoreRadius)
    {
        int x = (int)CurrentCameraCoords.x;
        int y = (int)CurrentCameraCoords.y;
        Vector2 foundChunk = Vector2.zero;
        float distanceToClosestChunk = 10000;
        for (int sX = x + radius; sX > x - radius; sX--)
        {
            for (int sY = y + radius; sY > y - radius; sY--)
            {
                Vector2 vector = new Vector2(sX, sY);
                if ((sX < x + ignoreRadius && sX > x - ignoreRadius) && (sY < y + ignoreRadius && sY > y - ignoreRadius))
                {
                    continue;
                }
                if (chunkId == GetSeededChunkId(vector))
                {
                    float distanceToFoundChunk = Vector2.Distance(currentCameraCoords, vector);
                    if (distanceToFoundChunk < distanceToClosestChunk)
                    {
                        distanceToClosestChunk = distanceToFoundChunk;
                        foundChunk = vector;
                    }
                }
            }
        }
        return foundChunk;
    }


    private Dictionary<Vector2,bool> CreateCollisonMap(int x, int y, int repeats)
    {
        Dictionary<Vector2, bool> chunksEligableForSpawn = new Dictionary<Vector2, bool>();
        do
        {
            //placeLocations(map.locations, map);


            int maxTunnels = 8; // max number of tunnels possible
            int maxLength = 4; // max length each tunnel can have

            int currentRow = 0;//(int)Mathf.Floor(Random.Range(15,x-15)); // our current row - start at a random spot
            int currentColumn = 0; //(int)Mathf.Floor(Random.Range(15, y-15)); // our current column - start at a random spot
            Vector2 lastDirection = Vector2.zero; // save the last direction we went
            Vector2 randomDirection; // next turn/direction - holds a value from directions

            // lets create some tunnels - while maxTunnels, dimentions, and maxLength  is greater than 0.

            while (maxTunnels > 0 && x > 0 && y > 0 && maxLength > 0 /*checkLocationConnection(map) < map.locations.Count-1*/)
            {

                // lets get a random direction - until it is a perpendicular to our lastDirection
                // if the last direction = left or right,
                // then our new direction has to be up or down,
                // and vice versa
                do
                {
                    int rnd = (int)Mathf.Round(seed.Next(-1,2));
                    int apply = (int)Mathf.Ceil(seed.Next(-1, 2));
                    if (apply == 0)
                    {
                        randomDirection = new Vector2(0, rnd);
                    }
                    else
                    {
                        randomDirection = new Vector2(rnd, 0);
                    }
                } while ((randomDirection.x == -lastDirection.x && randomDirection.y == -lastDirection.y) || (randomDirection.x == lastDirection.x && randomDirection.y == lastDirection.y));

                int randomLength = (int)Mathf.Ceil(seed.Next(1,maxLength)); //length the next tunnel will be (max of maxLength)
                int tunnelLength = 0; //current length of tunnel being created

                // lets loop until our tunnel is long enough or until we hit an edge
                while (tunnelLength < randomLength)
                {


                    //break the loop if it is going out of the mapCoords
                    if (((currentRow == 30) && (randomDirection.x == -1)) ||
                        ((currentColumn == 30) && (randomDirection.y == -1)) ||
                        ((currentRow == x - 31) && (randomDirection.x == 1)) ||
                        ((currentColumn == y - 31) && (randomDirection.y == 1)))
                    {

                        break;
                    }
                    else
                    {
                        if (!chunksEligableForSpawn.ContainsKey(new Vector2(currentRow, currentColumn)))
                        {
                            print(new Vector2(currentRow, currentColumn));
                            chunksEligableForSpawn.Add(new Vector2(currentRow, currentColumn), true);
                            Instantiate(PublicGameResources.GetResource().bloodFx, new Vector3(currentRow * CHUNKSIZE, currentColumn * CHUNKSIZE, 0), Quaternion.identity); // TEMP FOR DEBUGING
                        }
                        currentRow += (int)randomDirection.x; //add the value from randomDirection to row and col (-1, 0, or 1) to update our location
                        currentColumn += (int)randomDirection.y;
                        tunnelLength++; //the tunnel is now one longer, so lets increment that variable

                    }
                }

                if (tunnelLength > 0)
                { // update our variables unless our last loop broke before we made any part of a tunnel
                    lastDirection = randomDirection; //set lastDirection, so we can remember what way we went
                    maxTunnels--; // we created a whole tunnel so lets decrement how many we have left to create
                }
            }
            repeats--;
        }
        while (repeats > 0);
        return chunksEligableForSpawn; 
    }

    private int[,] CreateArray(int num, int x, int y)
    {
        int[,] array = new int[x, y];
        for (int xZ = 0; xZ < x; xZ++)
            for (int yZ = 0; yZ < y; yZ++)
            {
                array[xZ, yZ] = 1;

            }
        return array;
    }

    private void CheckSeedMap(Vector2 coords)
    {
        if (coords.x >= 0)
        {
            while (xSeedMap.Count < coords.x + 1)
            {
                WriteSeedIntoSeedMap(xSeedMap, xSeed);
            }
        }
        else
        {
            while (negativeXSeedMap.Count < Mathf.Abs(coords.x) + 1)
            {
                WriteSeedIntoSeedMap(negativeXSeedMap, negativeXSeed);
            }
        }
        if (coords.y >= 0)
        {
            while (ySeedMap.Count < coords.y + 1)
            {
                WriteSeedIntoSeedMap(ySeedMap, ySeed);
            }
        }
        else
        {
            while (negativeYSeedMap.Count < Mathf.Abs(coords.y) + 1)
            {
                WriteSeedIntoSeedMap(negativeYSeedMap, negativeYSeed);
            }
        }
    }

    private int WriteSeedIntoSeedMap(List<int> seedMap,System.Random seed)
    {
        seedMap.Add(seed.Next());
        return 0;
    }
}


public class Map
{

    public int[,] coords;
    public int xDim;
    public int yDim;
    public int[] entryPoint = { 0, 0 };
    public int[] exitPoint = { 50, 50 };


}

