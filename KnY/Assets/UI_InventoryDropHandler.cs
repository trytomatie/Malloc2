using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventoryDropHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler,IPointerExitHandler
{
    public bool isPersistent = true;
    public List<Inventory> inventorysThatHavePermission;
    public bool allowsArtifactItems = false;
    public bool allowsNonArtifactItems = false;
    public bool isInactiveArtifactInventorySlot = false;
    public void OnDrop(PointerEventData eventData)
    {
        if(UI_InventoryDragHandler.currentlyDragging != null)
        {
            bool permission = false;
            Item dragItem = UI_InventoryDragHandler.currentlyDragging.GetComponent<UI_ArtifactDisplayOnHover>().item;
            Item myItem = null;
            if (gameObject.transform.childCount > 0)
            { 
                myItem = gameObject.transform.GetChild(0).gameObject.GetComponent<UI_ArtifactDisplayOnHover>().item;
            }


            foreach (Inventory inv in inventorysThatHavePermission)
            {
                if(dragItem.owner == inv)
                {
                    permission = true;
                }
            }


            if(permission)
            { 
                if(dragItem.artifactItem == true)
                {
                    permission = allowsArtifactItems;
                }
                if (dragItem.artifactItem == false)
                {
                    permission = allowsNonArtifactItems;
                }
            }


            if (!permission)
            {
                return;
            }

            if (gameObject.transform.childCount > 0) // do I have an Item attached to me?
            {
                if(dragItem.artifactItem == myItem.artifactItem && dragItem.owner == myItem.owner)
                {
                    if(((isInactiveArtifactInventorySlot && !UI_InventoryDragHandler.currentlyDragging.transform.parent.GetComponent<UI_InventoryDropHandler>().isInactiveArtifactInventorySlot) 
                        || (!isInactiveArtifactInventorySlot && UI_InventoryDragHandler.currentlyDragging.transform.parent.GetComponent<UI_InventoryDropHandler>().isInactiveArtifactInventorySlot)) && isPersistent)
                    { 
                        if(isInactiveArtifactInventorySlot)
                        { 
                            dragItem.owner.GetComponent<Inventory>().SwitchArtifactInInventorys(dragItem, myItem);
                        }
                        else
                        {
                            dragItem.owner.GetComponent<Inventory>().SwitchArtifactInInventorys(myItem, dragItem);
                        }
                    }
                    else
                    {
                        int pos1 = dragItem.position;
                        dragItem.position = myItem.position;
                        myItem.position = pos1;
                    }
                    UI_InventoryManager.ClearInventoryDisplays();
                    UI_InventoryManager.FillInventoryDisplays();
                }
                return;
            }




            if (((isInactiveArtifactInventorySlot && !UI_InventoryDragHandler.currentlyDragging.transform.parent.GetComponent<UI_InventoryDropHandler>().isInactiveArtifactInventorySlot)
                       || (!isInactiveArtifactInventorySlot && UI_InventoryDragHandler.currentlyDragging.transform.parent.GetComponent<UI_InventoryDropHandler>().isInactiveArtifactInventorySlot)) && isPersistent)
                       // Check if i Transfer an Item from an Inactive Artifact slot, to an Active one / Vice versa
            {
                if (isInactiveArtifactInventorySlot)
                {
                    dragItem.owner.GetComponent<Inventory>().SwitchArtifactInInventorys(dragItem, true);
                }
                else
                {
                    dragItem.owner.GetComponent<Inventory>().SwitchArtifactInInventorys(dragItem, false);
                }
            }


            UI_InventoryDragHandler.currentlyDragging.transform.SetParent(gameObject.transform);
            gameObject.transform.GetChild(0).transform.localPosition = Vector2.zero;
            if (isPersistent)
            {
                int i = 0;
                for (i = 0; i < gameObject.transform.parent.childCount; i++)
                {
                    if (gameObject.transform.parent.GetChild(i) == gameObject.transform)
                    {
                        break;
                    }
                }
                dragItem.position = i + 1;
                UI_InventoryManager.ClearInventoryDisplays();
                UI_InventoryManager.FillInventoryDisplays();
            }
        }
        if (UI_TraderInventory.instance != null && UI_TraderInventory.instance.gameObject.activeSelf)
        {
            UI_TraderInventory.instance.UpdateTraderLists();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color32(255, 255, 255, 110);
    }
}
