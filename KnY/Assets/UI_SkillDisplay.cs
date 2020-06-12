using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Skill skill;
    private GameObject descriptionDisplay;
    private bool mouseEntered;
    public Text cooldownText;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(skill == null)
        {
            return;
        }
        mouseEntered = true;
        DescriptionDisplay.GetComponent<RectTransform>().localScale = transform.parent.GetComponent<RectTransform>().localScale;
        DescriptionDisplay.GetComponent<UI_ArtifactDisplayDescriptionPopup>().text.text = skill.Name + "\n Cost: " + skill.SpCost + "\n" +
            "Description: " + skill.Description;
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

        if(skill.CooldownTimer > 0)
        {
            cooldownText.text = skill.CooldownTimer.ToString("0");
            GetComponent<Image>().color = new Color32(111, 111, 111,255);
        }
        else
        {
            cooldownText.text = "";
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
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
