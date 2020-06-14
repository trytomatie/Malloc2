using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PassiveSkillDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PassiveSkill skill = null;
    private GameObject descriptionDisplay;
    private bool mouseEntered;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Skill == null)
        {
            return;
        }
        mouseEntered = true;
        DescriptionDisplay.GetComponent<UI_ArtifactDisplayDescriptionPopup>().text.text = Skill.Name + "\n" + Skill.Description;
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
                GetComponent<Image>().sprite = ItemIcons.GetSkillIcon(skill.ImageId);
            }
            else
            {
                GetComponent<Image>().sprite = ItemIcons.GetSkillIcon(0) ;
            }
        }
    }
}
