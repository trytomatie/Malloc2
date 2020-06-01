using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the display of Artifacts in UI
/// </summary>
public class UI_StatuseffectManager : MonoBehaviour
{
    public List<GameObject> artifactDisplays;
    public Statusmanager playerStatusmanager;
    public GameObject statusEffectDisplayInstantiationTarget;

    private static List<UI_StatuseffectManager> instances = new List<UI_StatuseffectManager>();
    
    // Start is called before the first frame update
    void Awake()
    {
        instances.RemoveAll(item => item == null);
        instances.Add(this);
        playerStatusmanager = FindObjectOfType<PlayerController>().GetComponent<Statusmanager>();
    }

    void OnEnable()
    {
        ClearStatusEffectDisplay();
        FillStatusEffectDisplay();
    }

    /// <summary>
    /// Clears Artifact-Displays
    /// </summary>
    public static void ClearStatusEffectDisplay()
    {
        foreach(UI_StatuseffectManager instance in instances)
        { 
            List<GameObject> removalList = new List<GameObject>();
            foreach(GameObject go in instance.artifactDisplays)
            {
                removalList.Add(go);
            }
            foreach(GameObject go in removalList)
            {
                instance.artifactDisplays.Remove(go);
                Destroy(go);
            }
        }
    }

    /// <summary>
    /// Fills Arifact-Displays
    /// </summary>
    public static void FillStatusEffectDisplay()
    {
        foreach (UI_StatuseffectManager instance in instances)
        {
            int xPos = 40;
            int yPos = 0;
            foreach (StatusEffect statuseffect in instance.playerStatusmanager.statusEffects)
            {
                if(!statuseffect.hidden)
                { 
                    GameObject instanceDisplay = Instantiate(instance.statusEffectDisplayInstantiationTarget, instance.transform);
                    instanceDisplay.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPos);
                    instanceDisplay.GetComponent<RectTransform>().localPosition = new Vector3(instanceDisplay.GetComponent<RectTransform>().localPosition.x, instanceDisplay.GetComponent<RectTransform>().localPosition.y, -1);
                    instanceDisplay.GetComponent<Image>().sprite = statuseffect.image;
                    instanceDisplay.GetComponent<Image>().material = PublicGameResources.GetResource().statusEffectMaterials[(int)statuseffect.type];
                    instanceDisplay.GetComponent<UI_StatusEffectDisplayOnHover>().statusEffect = statuseffect;
                    instance.artifactDisplays.Add(instanceDisplay);
                    xPos += 65;
                }
            }
        }
    }
}
