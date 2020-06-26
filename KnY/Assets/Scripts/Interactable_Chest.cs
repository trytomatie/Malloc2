using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Chest : Interactable_BlankChest {

    public int chanceForCommonItem = 90;
    public int chanceForUncommonItem = 10;
    public int chanceForRareItem = 0;
    public int chanceForEpicItem = 0;
    public int chanceForLegendaryItem = 0;
	// Use this for initialization
	void Start () {
        GetComponent<Interactable>()._customInteractableMethod = Interact;
        GetComponent<Interactable>()._interactablePopupMessage += " (" + cost + " mana)";
        GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Interact(GameObject g)
    {
        if(g.GetComponent<Statusmanager>().Mana < cost)
        {
            return;
        }
        g.GetComponent<Statusmanager>().Mana -= cost;
        GetComponent<Animator>().SetBool("isOpen", true);
        GetComponent<Interactable>().Disabled = true;
        GetComponent<Interactable>()._customInteractableMethod = SecondInteract;
        if(spawnItem != null)
        {
            StartCoroutine(SpawnItem());
        }
    }

    private void SecondInteract(GameObject g)
    {
        GetComponent<Interactable>().Disabled = true;
    }

    IEnumerator SpawnItem()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        GameObject item = Instantiate(spawnItem,transform.position,Quaternion.identity);
        int max = chanceForCommonItem + chanceForUncommonItem + chanceForRareItem + chanceForEpicItem + chanceForLegendaryItem;
        float chance = UnityEngine.Random.Range(0.0f, max);
        int id = 0;
        if (chance < chanceForCommonItem)
        { 
            id = Item.GenerateRandomCommonItemID();
        }
        else
        {
            chance -= chanceForCommonItem;
            if (chance < chanceForUncommonItem)
            { 
                id = Item.GenerateRandomUncommonItemID();
            }
            else
            {
                chance -= chanceForUncommonItem;
                if (chance < chanceForRareItem)
                {
                    id = Item.GenerateRandomRareItemID();
                }
                else
                {
                    chance -= chanceForRareItem;
                    if (chance < chanceForEpicItem)
                    {
                        id = Item.GenerateRandomEpicItemID();
                    }
                    else
                    {
                        id = Item.GenerateRandomLegendaryItemID();
                    }

                }
            }
        }
        item.transform.GetChild(0).GetComponent<Interactable_Item>()._itemId = id;
        item.transform.GetChild(0).GetComponent<SpriteRenderer>().material = Item.GetItemMaterial(id);
    }
}
