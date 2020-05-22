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
        GetComponent<SpriteRenderer>().sprite = GameObject.FindObjectOfType<ItemIcons>().GetIcon(_itemId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Interact(GameObject g)
    {
        Item item = Item.GenerateItem(_itemId);
        // TODO: make it dynamic for other GameObject interactors
        GameObject.Find("Player").GetComponent<Inventory>().AddItem(item);
        GameObject.FindObjectOfType<ItemDescriptionManager>().SetDescriptionProperties(item.itemName, item.description, GameObject.FindObjectOfType<ItemIcons>().GetIcon(_itemId),Item.GetItemDescriptionMaterial(_itemId));
        GameObject.FindObjectOfType<ItemDescriptionManager>().Show(5f);
        Destroy(gameObject);
    }
}
