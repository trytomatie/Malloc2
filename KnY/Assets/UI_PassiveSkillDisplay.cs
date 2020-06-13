using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PassiveSkillDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PassiveSkill skill = null;
    private GameObject descriptionDisplay;
    private bool mouseEntered;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(skill == null)
        {
            return;
        }
        mouseEntered = true;
        DescriptionDisplay.GetComponent<UI_ArtifactDisplayDescriptionPopup>().text.text = skill.Name + "\n" + skill.Description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (skill == null)
        {
            return;
        }
        UI_ArtifactDisplayDescriptionPopup.DespawnAllInstances();
        mouseEntered = false;
    }

    private void Update()
    {
        if (skill == null)
        {
            return;
        }
        if (mouseEntered)
        {
            DescriptionDisplay.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
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

}
