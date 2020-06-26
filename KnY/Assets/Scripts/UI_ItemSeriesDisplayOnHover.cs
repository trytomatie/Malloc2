using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handles Artifact Descriptions
/// </summary>
public class UI_ItemSeriesDisplayOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private GameObject descriptionDisplay;
    private GameObject contextMenu;
    private bool mouseEntered = false;
    public ItemSeries itemSeries;


    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEntered = true;
        string description = "";
        string color = "<color=white>";
        string colorEnd = "</color>";
        int i = 0;
        foreach(String desc in itemSeries.description)
        {
            if(itemSeries.conditionsNeeded[i] <= itemSeries.totalConditionsMet)
            {
                color = "<color=white>";
            }
            else
            {
                color = "<color=grey>";
            }
            description += "\n" + color + "(" + itemSeries.conditionsNeeded[i] +") "+ desc + colorEnd;
            i++;
        }
        DescriptionDisplay.GetComponent<RectTransform>().localScale = transform.parent.GetComponent<RectTransform>().localScale;
        DescriptionDisplay.GetComponent<UI_ArtifactDisplayDescriptionPopup>().text.text = itemSeries.seriesName + "\n" +
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
        //if(eventData.button == PointerEventData.InputButton.Right)
        //{
        //    ContextMenu.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    statusEffect.InstanciateContextMenu();
        //}
    }

    public GameObject DescriptionDisplay
    {
        get
        {
            if (descriptionDisplay == null)
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
}
