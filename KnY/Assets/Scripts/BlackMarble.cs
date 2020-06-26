using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackMarble : MonoBehaviour
{
    private MapGenerator mapGenerator;
    private Vector2 lastSearchPos = Vector2.zero;
    public Dictionary<Vector2,int> searchObjectList = new Dictionary<Vector2, int>();
    public Vector2 direction;

    public List<Material> myMaterial = new List<Material>();
    public Material interfaceMaterial;
    [ColorUsage(true,true)]
    public Color commonGlow;
    [ColorUsage(true, true)]
    public Color unCommonGlow;
    [ColorUsage(true, true)]
    public Color bossGlow;

    public List<int> commontreasureChunkIds;
    public List<int> uncommontreasureChunkIds;
    public List<int> chunkIdsThatAreSearched = new List<int>();
    public bool canLocateBosses = true;
    public Item_BlackMarble itemRef;
    // Start is called before the first frame update
    void Start()
    {
        if(mapGenerator == null)
        {
            mapGenerator = FindObjectOfType<MapGenerator>();
            if (mapGenerator == null)
            {
                return;
            }
        }
        interfaceMaterial.SetFloat("_fade", 1);
        SearchAllCommonTreasureChunks();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 myPos = transform.position;
        if (mapGenerator != null)
        {
            myPos = mapGenerator.CurrentCameraCoords;
        }
        direction = PublicGameResources.CalculateNormalizedDirection(myPos, GetClosestTarget());
    }

    public Vector2 GetClosestTarget() // Depricated, skip that shit
    {
        float distance = 100000;
        Vector2 target = transform.position;
        if (mapGenerator != null)
        {
            target = mapGenerator.CurrentCameraCoords;
            int foundChunkId = 0;
            int vFoundChunkId = 0;
            foreach (Vector2 v in searchObjectList.Keys)
            {

                if (searchObjectList[v] == -1 || chunkIdsThatAreSearched.Count < 1 || !mapGenerator.tunnelMap.Keys.Contains(v))
                {
                    continue;
                }
                foundChunkId = -1;
                foreach (int chunkId in chunkIdsThatAreSearched)
                {

                    if (chunkId == searchObjectList[v])
                    {
                        foundChunkId = searchObjectList[v];
                        break;
                    }
                }
                if (foundChunkId == -1)
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
                        if (mapGenerator.chunkMap[v].GetComponent<ChunkSettings>().Concluded)
                        {
                            searchObjectList[v] = -1;
                        }
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
        }
        target = LocateBoss(distance,target);
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

    public Vector2 LocateBoss(float distance,Vector2 pDirection)
    {
        if(!canLocateBosses)
        {
            return pDirection;
        }
        GameObject searchTarget = null;
        foreach(AI_ThunderBirdBossEnemy g in GameObject.FindObjectsOfType<AI_ThunderBirdBossEnemy>())
        {
            if(g.GetComponent<Statusmanager>().Hp > 0)
            { 
                float newDistance = Vector2.Distance(transform.position, g.transform.position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    searchTarget = g.gameObject;
                }
            }
        }
        if (searchTarget != null)
        {
            interfaceMaterial.SetColor("_color", bossGlow);
            pDirection = searchTarget.transform.position;

        }
        return pDirection;

    }
}
