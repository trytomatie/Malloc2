using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Chest : MonoBehaviour {

    public GameObject spawnItem;
    public int chanceForCommonItem = 90;
    public int chanceForUncommonItem = 10;
    public int chanceForRareItem = 0;
    public int chanceForEpicItem = 0;
	// Use this for initialization
	void Start () {
        GetComponent<Interactable>()._customInteractableMethod = Interact;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Interact()
    {
        GetComponent<Animator>().SetBool("isOpen", true);
        GetComponent<Interactable>().Disabled = true;
        GetComponent<Interactable>()._customInteractableMethod = SecondInteract;
        GetComponent<Interactable>()._interactablePopupMessage = "Take";
        if(spawnItem != null)
        {
            StartCoroutine(SpawnItem());
        }
    }

    private void SecondInteract()
    {
        GetComponent<Interactable>().Disabled = true;
    }

    IEnumerator SpawnItem()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        GameObject item = Instantiate(spawnItem,transform.position,Quaternion.identity);
        int max = chanceForCommonItem + chanceForUncommonItem + chanceForRareItem + chanceForEpicItem + 1;
        int chance = UnityEngine.Random.Range(0, max);
        int id = 0;
        if (chance < chanceForCommonItem)
        { 
            id = Item.GenerateRandomCommonItemID();
        }
        else
        {
            max -= chanceForCommonItem;
            chance = UnityEngine.Random.Range(0, max);
            if (chance < chanceForUncommonItem)
            { 
                id = Item.GenerateRandomUncommonItemID();
            }
            else
            {
                max -= chanceForRareItem;
                chance = UnityEngine.Random.Range(0, max);
                if (chance < chanceForRareItem)
                {
                    id = Item.GenerateRandomRareItemID();
                }
                else
                {
                    id = Item.GenerateRandomEpicItemID();
                }
            }
        }
        item.transform.Find("ItemObject").GetComponent<Interactable_Item>()._itemId = id;
        item.transform.Find("ItemObject").GetComponent<SpriteRenderer>().material = Item.GetItemMaterial(id);
    }
}
