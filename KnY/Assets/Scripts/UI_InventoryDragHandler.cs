using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventoryDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler,IBeginDragHandler
{
    public static GameObject currentlyDragging;
    public Vector2 myLastPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        myLastPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(transform.parent.GetComponent<UI_InventoryDropHandler>().IsFrozen)
        {
            return;
        }
        currentlyDragging = gameObject;
        GetComponent<Image>().raycastTarget = false;
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentlyDragging = null;
        GetComponent<Image>().raycastTarget = true;
        if(transform.parent.GetComponent<UI_InventoryDropHandler>() != null)
        {
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.position = myLastPosition;
        }
    }

}
