using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the display of Artifacts in UI
/// </summary>
public class UI_ItemSeriesManager : MonoBehaviour
{
    public List<GameObject> itemSeriesDisplays = new List<GameObject>();
    public static Inventory playerInventory;
    public GameObject itemSeriesDisplayInstantiationTarget;
    private static List<UI_ItemSeriesManager> instances = new List<UI_ItemSeriesManager>();



    // Start is called before the first frame update
    void Awake()
    {
        Instances.RemoveAll(item => item == null);
        Instances.Add(this);
    }

    void OnEnable()
    {
        ClearItemSeriesDisplays();
        FillItemSeriesDisplays();
    }

    /// <summary>
    /// Clears Inventory-Displays
    /// </summary>
    public static void ClearItemSeriesDisplays()
    {

        foreach(UI_ItemSeriesManager instance in Instances)
        { 
            List<GameObject> removalList = new List<GameObject>();
            foreach(GameObject go in instance.itemSeriesDisplays)
            {
                if(go != null)
                {
                    removalList.Add(go);
                }

            }
            foreach(GameObject go in removalList)
            {
                instance.itemSeriesDisplays.Remove(go);
                go.transform.SetParent(null);
                Destroy(go);
            }
        }
    }

    /// <summary>
    /// Fills Inventory-Displays
    /// </summary>
    public static void FillItemSeriesDisplays()
    {

        foreach (UI_ItemSeriesManager instance in Instances)
        {
            int pos = -40;
            int posProgress = -70;
            foreach (ItemSeries series in instance.PlayerInventory.itemSeries.Values)
            {
                if(series.totalConditionsMet >0)
                { 
                    GameObject g = Instantiate(instance.itemSeriesDisplayInstantiationTarget, instance.transform);

                    g.GetComponent<UI_ItemSeriesDisplay>().series = series;
                    instance.itemSeriesDisplays.Add(g);
                }
            }
            instance.itemSeriesDisplays.Sort((a,b) => b.GetComponent<UI_ItemSeriesDisplay>().series.totalConditionsMet.CompareTo(a.GetComponent<UI_ItemSeriesDisplay>().series.totalConditionsMet));
            foreach (GameObject g in instance.itemSeriesDisplays)
            {
                g.GetComponent<RectTransform>().anchoredPosition = new Vector3(140, pos, -1);
                pos += posProgress;
            }
        }
    }
    public Inventory PlayerInventory
    {
        get
        {
            if(playerInventory == null)
            {
                playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
            }
            return playerInventory;
        }

        set
        {
            playerInventory = value;
        }
    }

    public static List<UI_ItemSeriesManager> Instances
    {
        get
        {
            instances.RemoveAll(item => item == null);
            return instances;
        }

        set
        {
            instances = value;
        }
    }
}
