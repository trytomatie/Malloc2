using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handles Artifact Descriptions
/// </summary>
public class UI_ArtifactDisplayOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject descriptionDisplay;
    private bool mouseEntered = false;
    public Item item;

    public GameObject DescriptionDisplay
    {
        get
        {
            if(descriptionDisplay == null)
            {
                descriptionDisplay = GameObject.Find("ItemDescriptionWindow");
            }
            return descriptionDisplay;
        }

        set
        {
            descriptionDisplay = value;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEntered = true;
        DescriptionDisplay.GetComponent<RectTransform>().localScale = transform.parent.GetComponent<RectTransform>().localScale;
        DescriptionDisplay.GetComponent<UI_ArtifactDisplayDescriptionPopup>().text.text = item.itemName + "\n\n" + item.description;
    }

    public void Update()
    {
        if(mouseEntered)
        {
            DescriptionDisplay.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI_ArtifactDisplayDescriptionPopup.DespawnAllInstances();
        mouseEntered = false;
    }
}
