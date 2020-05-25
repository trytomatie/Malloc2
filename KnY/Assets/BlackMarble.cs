using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackMarble : MonoBehaviour
{
    private MapGenerator mapGenerator;
    private Vector2 lastSearchPos = Vector2.zero;
    public Dictionary<Vector2,bool> searchObjectList = new Dictionary<Vector2, bool>();
    public Vector2 direction;

    public List<Material> myMaterial = new List<Material>();
    public Material interfaceMaterial;
    [ColorUsage(true,true)]
    public Color commonGlow;
    [ColorUsage(true, true)]
    public Color unCommonGlow;

    public List<int> commontreasureChunkIds;
    public List<int> uncommontreasureChunkIds;
    public List<int> chunkIdsThatAreSearched = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        if(mapGenerator == null)
        {
            mapGenerator = FindObjectOfType<MapGenerator>();
        }
        interfaceMaterial.SetFloat("_fade", 1);
        SearchAllCommonTreasureChunks();
    }

    // Update is called once per frame
    void Update()
    {
        direction = PublicGameResources.CalculateNormalizedDirection(mapGenerator.CurrentCameraCoords, GetClosestTarget());
    }

    public Vector2 GetClosestTarget()
    {
        float distance = 100000;
        Vector2 target = mapGenerator.CurrentCameraCoords;
        int foundChunkId = 0;
        int vFoundChunkId = 0;
        foreach (Vector2 v in searchObjectList.Keys)
        {
            
            if (searchObjectList[v] == true || chunkIdsThatAreSearched.Count < 1 || !mapGenerator.tunnelMap.Keys.Contains(v))
            {
                continue;
            }
            foundChunkId = -1;
            foreach (int chunkId in chunkIdsThatAreSearched)
            {
                int id = mapGenerator.GetSeededChunkId(v);
                if (id == chunkId)
                {
                    foundChunkId = id;
                    break;
                }
            }
            if(foundChunkId == -1)
            {
                continue;
            }
            float vDistance = Vector2.Distance(v, mapGenerator.CurrentCameraCoords);
            if (distance > vDistance)
            {
                distance = vDistance;
                target = v;
                vFoundChunkId = foundChunkId;
                if (distance == 0)
                {
                    if(mapGenerator.chunkMap[v].GetComponent<ChunkSettings>().concluded)
                    {
                        searchObjectList[v] = true;
                    }
                    return target;
                }
            }
        }
        if (commontreasureChunkIds.Contains(vFoundChunkId))
        {
            interfaceMaterial.SetColor("_color", commonGlow);
        }
        if (uncommontreasureChunkIds.Contains(vFoundChunkId))
        {
            interfaceMaterial.SetColor("_color", unCommonGlow);
        }
        return target;
    }

    public void SearchAllCommonTreasureChunks()
    {
        chunkIdsThatAreSearched.Clear();
        chunkIdsThatAreSearched = chunkIdsThatAreSearched.Concat(commontreasureChunkIds).ToList<int>();
        mapGenerator.GetAllChunksOfType(commontreasureChunkIds, 10, 0, searchObjectList);
    }
    public void SearchAllUncommonTreasureChunks()
    {
        chunkIdsThatAreSearched.Clear();
        chunkIdsThatAreSearched = chunkIdsThatAreSearched.Concat(uncommontreasureChunkIds).ToList<int>();
        mapGenerator.GetAllChunksOfType(uncommontreasureChunkIds, 10, 0, searchObjectList);
    }
    public void SearchAllCommonAndUncommonTreasureChunks()
    {
        List<int> chunks = new List<int>();
        chunks = commontreasureChunkIds.Concat(uncommontreasureChunkIds).ToList<int>();
        chunkIdsThatAreSearched = chunks.ToList<int>();
        mapGenerator.GetAllChunksOfType(chunks, 10, 0, searchObjectList);
    }

    public void ClearChunkIdsThatAreSearched(List<int> removeList)
    {
        for(int i = 0; i < chunkIdsThatAreSearched.Count;i++)
        {
            foreach(int r in removeList)
            {
                if (chunkIdsThatAreSearched[i] == r)
                {
                    chunkIdsThatAreSearched.RemoveAt(i);
                }
            }
        }
    }
}
