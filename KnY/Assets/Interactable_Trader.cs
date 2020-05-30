using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Trader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>()._customInteractableMethod = Interact;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Interact(GameObject g)
    {
        GameObject traderInventory = UI_InputManager.Instance.traderInventory;
        UI_TraderInventory ui = traderInventory.GetComponent<UI_TraderInventory>();
        ui.traderInventory = GetComponent<Inventory>();
        traderInventory.SetActive(true);
        ui.ClearInventoryDisplays();
        ui.FillInventoryDisplays();
        Time.timeScale = 0;
    }
}
