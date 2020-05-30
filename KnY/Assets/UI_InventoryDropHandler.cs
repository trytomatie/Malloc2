using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InventoryDropHandler : MonoBehaviour, IDropHandler
{
    public bool isPersistent = true;
    public List<Inventory> inventorysThatHavePermission;
    public bool allowsArtifactItems = false;
    public bool allowsNonArtifactItems = false;
    public void OnDrop(PointerEventData eventData)
    {
        if(UI_InventoryDragHandler.currentlyDragging != null)
        {
            if(gameObject.transform.childCount > 0)
            {
                return;
            }


            bool permission = false;
            GameObject g = UI_InventoryDragHandler.currentlyDragging;
            foreach (Inventory inv in inventorysThatHavePermission)
            {
                if(g.GetComponent<UI_ArtifactDisplayOnHover>().item.owner == inv)
                {
                    permission = true;
                }
            }

            if(permission)
            { 
                if(g.GetComponent<UI_ArtifactDisplayOnHover>().item.artifactItem == true)
                {
                    permission = allowsArtifactItems;
                }
                if (g.GetComponent<UI_ArtifactDisplayOnHover>().item.artifactItem == false)
                {
                    permission = allowsNonArtifactItems;
                }
            }


            if (!permission)
            {
                return;
            }


            g.transform.SetParent(gameObject.transform);
            gameObject.transform.GetChild(0).transform.localPosition = Vector2.zero;
            if (isPersistent)
            {
                int i = 0;
                for (i = 0; i < gameObject.transform.parent.childCount;i++)
                {
                    if(gameObject.transform.parent.GetChild(i) == gameObject.transform)
                    {
                        break;
                    }
                }
                g.GetComponent<UI_ArtifactDisplayOnHover>().item.position = i+1;
            }
        }
        if(UI_TraderInventory.instance.gameObject.activeSelf)
        {
            UI_TraderInventory.instance.UpdateTraderLists();
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
