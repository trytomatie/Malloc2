using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PassiveSkillDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PassiveSkill skill = null;
    public GameObject descriptionDisplay;
    private bool mouseEntered;
    public bool hasDedicatedWindow;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Skill == null)
        {
            return;
        }
        mouseEntered = true;
        DescriptionDisplay.GetComponent<UI_ArtifactDisplayDescriptionPopup>().text.text = Skill.Name + "\n" + Skill.Description;
        DescriptionDisplay.GetComponent<RectTransform>().pivot = new Vector3(0, 1.1f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Skill == null)
        {
            return;
        }
        UI_ArtifactDisplayDescriptionPopup.DespawnAllInstances();
        DescriptionDisplay.GetComponent<RectTransform>().pivot = new Vector3(0, 0, 0);
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

    public PassiveSkill Skill
    {
        get
        {
            return skill;
        }

        set
        {
            skill = value;
            if (skill != null)
            {
                if(hasDedicatedWindow)
                {
                    DescriptionDisplay.GetComponent<UI_ArtifactDisplayDescriptionPopup>().text.text = skill.Name + "\n" + skill.Description;
                }
                GetComponent<Image>().sprite = ItemIcons.GetSkillIcon(skill.ImageId);
            }
            else
            {
                GetComponent<Image>().sprite = ItemIcons.GetSkillIcon(0) ;
            }
        }
    }
}
