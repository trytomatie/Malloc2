using Pathfinding;
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
    public int combinedLikelyhood;


    public System.Random seed = new System.Random(1337);
    private System.Random xSeed;
    private System.Random ySeed;
    private System.Random negativeXSeed;
    private System.Random negativeYSeed;

    private List<int> xSeedMap = new List<int>();
    private List<int> ySeedMap = new List<int>();
    private List<int> negativeXSeedMap = new List<int>();
    private List<int> negativeYSeedMap = new List<int>();

    private Dictionary<Vector2, GameObject> chunkMap = new Dictionary<Vector2, GameObject>();
    private Dictionary<Vector2, GameObject> exploredChunks = new Dictionary<Vector2, GameObject>();
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
                LoadChunk(toLoad + new Vector2(x, y), GetSeededChunkId(toLoad + new Vector2(x, y)) );
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
    void Start()
    {
        SetSeeds();
        foreach(int l in likelyhoodTable)
        {
            combinedLikelyhood += l;
        }
    }

    void SetSeeds()
    {
        xSeed = new System.Random(seed.Next());
        ySeed = new System.Random(seed.Next());
        negativeXSeed = new System.Random(seed.Next());
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

    private int GetSeededChunkId(Vector2 coords)
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
            x = negativeXSeedMap[(int)Mathf.Abs(coords.x)-1];
        }
        if (coords.y >= 0)
        {
            y = ySeedMap[(int)coords.y];
        }
        else
        {
            y = negativeYSeedMap[(int)Mathf.Abs(coords.y) - 1];
        }
        // print(x + " | " + y);
        int chunkId = 0; // new System.Random(x + y).Next(0, chunkTiles.Count);
        int likelyHood = new System.Random(x + y).Next(0, combinedLikelyhood+1);
        int i = 0;
        int likelyhoodMemory = 0;
        // print(likelyHood);
        foreach(int l in likelyhoodTable)
        {
            if(likelyHood + likelyhoodMemory <= l + likelyhoodMemory)
            {
                chunkId = i;
            }
            likelyhoodMemory += l;
            i++;
        }
        return chunkId;
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
