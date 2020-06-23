using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Item : MonoBehaviour
{
    public int _itemId;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>()._customInteractableMethod = Interact;
        GetComponent<Interactable>()._customAlternateInteractableMethod = AlternateInteract;
        GetComponent<SpriteRenderer>().sprite = GameObject.FindObjectOfType<ItemIcons>().GetIcon(_itemId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Interact(GameObject g)
    {
        if(g.GetComponent<Inventory>().ContainsItem(_itemId, 0))
        {
            UI_InfoTitleManager.Show("Can't pick up any more of this Artifact", "",3);
            return;
        }
        else if (g.GetComponent<Inventory>().ContainsItem(_itemId, 1))
        {
            UI_InfoTitleManager.Show("Can't pick up any more of this Artifact", "", 3);
            return;
        }

        if (g.GetComponent<Inventory>().artifactItemsCount <= 6)
        {
            AddItem(g);
            return;
        }
        else if(g.GetComponent<Inventory>().InactiveItemList().Count <= 6)
        {
            AddInactiveItem(g);
            return;
        }
        else
        {
            UI_InfoTitleManager.Show("Can't pick up any more Artifacts", "Disenchant for more space", 3);
            return;
        }
    }

    private void AddInactiveItem(GameObject g)
    {
        Item item = Item.GenerateItem(_itemId);
        g.GetComponent<Inventory>().AddInactiveArtifact(item);
        item.PickUpEffect(g);
        string description = item.description;
        if (Options.detailedDescriptions == 1 && item.detailedDescription != "")
        {
            description = item.detailedDescription;
        }
        GameObject.FindObjectOfType<ItemDescriptionManager>().SetDescriptionProperties(item.itemName + " (inactive) ", description, GameObject.FindObjectOfType<ItemIcons>().GetIcon(_itemId), Item.GetItemDescriptionMaterial(_itemId));
        GameObject.FindObjectOfType<ItemDescriptionManager>().Show(5f);
        Destroy(gameObject);
    }

    private void AddItem(GameObject g)
    {
        Item item = Item.GenerateItem(_itemId);
        g.GetComponent<Inventory>().AddItem(item);
        item.PickUpEffect(g);
        string description = item.description;
        if (Options.detailedDescriptions == 1 && item.detailedDescription != "")
        {
            description = item.detailedDescription;
        }
        GameObject.FindObjectOfType<ItemDescriptionManager>().SetDescriptionProperties(item.itemName, description, GameObject.FindObjectOfType<ItemIcons>().GetIcon(_itemId), Item.GetItemDescriptionMaterial(_itemId));
        GameObject.FindObjectOfType<ItemDescriptionManager>().Show(5f);
        Destroy(gameObject);
    }

    private void AlternateInteract(GameObject g)
    {
        Item item = Item.GenerateItem(_itemId);
        Item tokens = Item.GenerateTokens(item);
        g.GetComponent<Inventory>().AddItem(tokens);
        GameObject.FindObjectOfType<ItemDescriptionManager>().SetDescriptionProperties(tokens.itemName, tokens.description, tokens.image, Item.GetItemDescriptionMaterial(tokens.itemId));
        GameObject.FindObjectOfType<ItemDescriptionManager>().Show(5f);
        Destroy(gameObject);
    }
}
