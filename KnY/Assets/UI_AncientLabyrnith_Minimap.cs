using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AncientLabyrnith_Minimap : MonoBehaviour
{
    public List<GameObject> minimap = new List<GameObject>();
    public GameObject minimapDisplayInstantiationTarget;
    public Transform minmapParent;
    public List<Sprite> mapIcons = new List<Sprite>();
    private static List<UI_AncientLabyrnith_Minimap> instances = new List<UI_AncientLabyrnith_Minimap>();
    // Start is called before the first frame update
    void Awake()
    {
        instances.Add(this);
    }

    public static void UpdateMinimap(Dictionary<Vector2, GameObject> chunks,Vector2 playerPos)
    {
        foreach (UI_AncientLabyrnith_Minimap instance in instances)
        {
            foreach(GameObject g in instance.minimap)
            {
                Destroy(g);
            }
            instance.minimap.Clear();
            foreach (Vector2 v in chunks.Keys)
            {
                GameObject g = Instantiate(instance.minimapDisplayInstantiationTarget, instance.minmapParent);
                g.GetComponent<RectTransform>().anchoredPosition = v *32f - playerPos *32f;
                ChunkSettings chunksSettings = chunks[v].GetComponent<ChunkSettings>();
                if (chunksSettings.adjustedExits[0])
                {
                    g.transform.GetChild(0).gameObject.SetActive(false);
                }
                if (chunksSettings.adjustedExits[2])
                {
                    g.transform.GetChild(1).gameObject.SetActive(false);
                }
                if (chunksSettings.adjustedExits[1])
                {
                    g.transform.GetChild(2).gameObject.SetActive(false);
                }
                if (chunksSettings.adjustedExits[3])
                {
                    g.transform.GetChild(3).gameObject.SetActive(false);
                }
                if(v == playerPos)
                {
                    g.transform.GetChild(4).GetComponent<Image>().sprite = instance.mapIcons[0];
                }
                else
                {
                    switch(chunksSettings.chunkType)
                    {
                        case ChunkSettings.ChunkType.Standard:
                            break;
                        case ChunkSettings.ChunkType.Boss:
                            g.transform.GetChild(4).GetComponent<Image>().sprite = instance.mapIcons[2];
                            break;
                        case ChunkSettings.ChunkType.Special:
                            g.transform.GetChild(4).GetComponent<Image>().sprite = instance.mapIcons[5];
                            break;
                        case ChunkSettings.ChunkType.Trader:
                            break;
                        case ChunkSettings.ChunkType.Treasure:
                            g.transform.GetChild(4).GetComponent<Image>().sprite = instance.mapIcons[1];
                            break;

                    }
                }
                instance.minimap.Add(g);
            }
        }
    }
}
