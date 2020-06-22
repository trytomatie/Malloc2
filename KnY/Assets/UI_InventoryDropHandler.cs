using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventoryDropHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler,IPointerExitHandler
{
    public List<Inventory> inventorysThatHavePermission;
    private bool isFrozen = false;
    public Vector2Int position;



    public void OnDrop(PointerEventData eventData)
    {
        if (UI_InventoryDragHandler.currentlyDragging == null)
        {
            return;
        }
        Vector2Int positionOfItemThatIsDragged = UI_InventoryDragHandler.currentlyDragging.transform.parent.GetComponent<UI_InventoryDropHandler>().position;
        Inventory targetInventory = UI_InventoryDragHandler.currentlyDragging.GetComponent<UI_ArtifactDisplayOnHover>().item.owner;
        Item draggedItem = UI_InventoryDragHandler.currentlyDragging.GetComponent<UI_ArtifactDisplayOnHover>().item;
        Item myItem = null;
        if (transform.childCount > 0)
        {
            myItem = transform.GetChild(0).GetComponent<UI_ArtifactDisplayOnHover>().item;
        }
        Transform otherDropHandler = UI_InventoryDragHandler.currentlyDragging.transform.parent;


        if (IsFrozen && draggedItem == myItem)
        {
            Destroy(otherDropHandler.GetChild(0).gameObject);
            IsFrozen = false;
        }

        if (inventorysThatHavePermission.Contains(targetInventory))
        {
            if(position.x < 0 && myItem == null)
            {
                if (positionOfItemThatIsDragged.x < 0 )
                {
                    
                }
                else
                { 
                    UI_TraderInventory.CreateInventoryDisplayForTrader(draggedItem, gameObject);
                    UI_InventoryDragHandler.currentlyDragging.transform.parent.GetComponent<UI_InventoryDropHandler>().IsFrozen = true;
                }
            }
            else if(!IsFrozen)
            { 
                targetInventory.SwapItemPositions(position,positionOfItemThatIsDragged);
            }
        }
        if(UI_TraderInventory.instance != null && UI_TraderInventory.instance.gameObject.activeSelf)
        {
            UI_TraderInventory.instance.UpdateTraderLists();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!IsFrozen)
        { 
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsFrozen)
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 110);
        }
    }
    public bool IsFrozen
    {
        get
        {

            return isFrozen;
        }

        set
        {
            if(value)
            {
                if (transform.childCount > 0)
                {
                    Material material = new Material(transform.GetChild(0).GetComponent<Image>().material);
                    material.SetColor("_color", new Color(0.40f, 0.40f, 0.40f, 0));
                    transform.GetChild(0).GetComponent<Image>().material = material;
                }
            }
            else
            {
                if(transform.childCount > 0)
                { 
                    Material material = new Material(transform.GetChild(0).GetComponent<Image>().material);
                    material.SetColor("_color", new Color(0f, 0f, 0f, 0));
                    transform.GetChild(0).GetComponent<Image>().material = material;
                }
            }
            isFrozen = value;
        }
    }


}
