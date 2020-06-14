using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Skill skill = null;
    private GameObject descriptionDisplay;
    private bool mouseEntered;
    public Text cooldownText;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Skill == null)
        {
            return;
        }
        mouseEntered = true;
        DescriptionDisplay.GetComponent<UI_ArtifactDisplayDescriptionPopup>().text.text = Skill.Name + "\nCost: " + Skill.SpCost + "\n" +
            "Description: " + Skill.Description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Skill == null)
        {
            return;
        }
        UI_ArtifactDisplayDescriptionPopup.DespawnAllInstances();
        mouseEntered = false;
    }

    private void Update()
    {
        if (Skill == null)
        {
            return;
        }
        if (mouseEntered)
        {
            DescriptionDisplay.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Skill.CooldownTimer > 0)
        {
            if(cooldownText != null)
            { 
            cooldownText.text = Skill.CooldownTimer.ToString("0");
            }
            GetComponent<Image>().color = new Color32(111, 111, 111,255);
        }
        else
        {
            if (cooldownText != null)
            {
                cooldownText.text = "";
            }
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

    public Skill Skill
    {
        get
        {
            return skill;
        }

        set
        {
            skill = value;
            if(skill != null)
            { 
                 GetComponent<Image>().sprite = skill.Image;
            }
            else
            {
                GetComponent<Image>().sprite = ItemIcons.GetSkillIcon(0);
            }
        }
    }
}
