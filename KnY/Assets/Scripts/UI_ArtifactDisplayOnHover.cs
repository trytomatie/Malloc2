using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handles Artifact Descriptions
/// </summary>
public class UI_ArtifactDisplayOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private GameObject descriptionDisplay;
    private GameObject contextMenu;
    private bool mouseEntered = false;
    public Item item;

    public GameObject DescriptionDisplay
    {
        get
        {
            if(descriptionDisplay == null)
            {
                descriptionDisplay = GameObject.Find("ItemDescriptionPopupWindow");
            }
            return descriptionDisplay;
        }

        set
        {
            descriptionDisplay = value;
        }
    }

    public GameObject ContextMenu
    {
        get
        {
            if (contextMenu == null)
            {
                contextMenu = GameObject.Find("ItemContextMenu");
            }
            return contextMenu;
        }

        set
        {
            contextMenu = value;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string series = "";
        foreach(ItemSeries.Series s in item.series)
        {
            series += s.ToString() + " ";
        }
        mouseEntered = true;
        DescriptionDisplay.GetComponent<RectTransform>().localScale = transform.parent.GetComponent<RectTransform>().localScale;
        string description = item.description;
        if(Options.detailedDescriptions == 1 && item.detailedDescription != "")
        {
            description = item.detailedDescription;
        }
        DescriptionDisplay.GetComponent<UI_ArtifactDisplayDescriptionPopup>().text.text = item.itemName + "\n" +
            "Atribute: " + item.attribute + "\n" +
            "Series: " + series + "\n\n" +
            description;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            ContextMenu.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            item.InstanciateContextMenu();
        }
    }
}
