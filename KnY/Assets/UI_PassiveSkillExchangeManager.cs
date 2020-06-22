using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PassiveSkillExchangeManager : MonoBehaviour
{
    private static List<UI_PassiveSkillExchangeManager> instances = new List<UI_PassiveSkillExchangeManager>();
    public UI_PassiveSkillDisplay passiveSkill1;
    public UI_PassiveSkillDisplay passiveSkill2;
    public UI_PassiveSkillDisplay newPassiveSkill;
    public Text costText;
    private static SkillManager mySkillmanager;
    private static int cost = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ClearAllSlots()
    {
        passiveSkill1.Skill = null;
        passiveSkill2.Skill = null;
        newPassiveSkill.Skill = null;
    }

    public static void OpenSkillExchangeWindow(PassiveSkill newSkill)
    {
        if(GameObject.FindObjectOfType<MapGenerator>() != null)
        { 
            cost = 5 * GameObject.FindObjectOfType<MapGenerator>().currentFloor;
        }
        else
        {
            cost = 5;
        }
        foreach (UI_PassiveSkillExchangeManager instance in Instances)
        {
            instance.costText.text = "Reroll \n(Cost: " + cost+")";
            instance.ClearAllSlots();
            if (MySkillmanager.PassiveSkills[0] !=  null)
            { 
                instance.passiveSkill1.Skill = MySkillmanager.PassiveSkills[0];
            }
            else
            {
                instance.passiveSkill1.Skill = null;
            }
            if (MySkillmanager.PassiveSkills[1] != null)
            {
                instance.passiveSkill2.Skill = MySkillmanager.PassiveSkills[1];
            }
            else
            {
                instance.passiveSkill2.Skill = null;
            }
            instance.newPassiveSkill.Skill = newSkill;
            instance.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public static void ExchangeSkill()
    {
        foreach (UI_PassiveSkillExchangeManager instance in Instances)
        {
            MySkillmanager.AddPassiveSkill(instance.newPassiveSkill.Skill);
            break;
        }
        CloseSkillExchagneWindow();
    }

    public static void RerollSkill()
    {
        if(MySkillmanager.GetComponent<Statusmanager>().Mana >= cost)
        { 
            MySkillmanager.GetComponent<Statusmanager>().Mana -= cost;
            cost = (int)(cost * 1.2f);

            foreach (UI_PassiveSkillExchangeManager instance in Instances)
            {
                instance.costText.text = "Reroll \n(Cost: " + cost+")";
                if (GameObject.FindObjectOfType<MapGenerator>() != null)
                {

                    instance.newPassiveSkill.Skill = PassiveSkill.GenerateRandomPassive(GameObject.FindObjectOfType<MapGenerator>().currentFloor, MySkillmanager.GetComponent<Statusmanager>().characterClass);
                }
                else
                {
                    instance.newPassiveSkill.Skill = PassiveSkill.GenerateRandomPassive(1, MySkillmanager.GetComponent<Statusmanager>().characterClass);
                }
            }
        }
    }

    public static void CloseSkillExchagneWindow()
    {
        foreach (UI_PassiveSkillExchangeManager instance in Instances)
        {
            instance.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public static List<UI_PassiveSkillExchangeManager> Instances
    {
        get
        {
            instances.RemoveAll(item => item == null);
            return instances;
        }

        set
        {
            instances = value;
        }
    }

    public static bool InstancesActive
    {
        get
        {

            foreach(UI_PassiveSkillExchangeManager instance in Instances)
            {
                if(instance.gameObject.activeSelf)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public static SkillManager MySkillmanager
    {
        get
        {
            if(mySkillmanager == null)
            {
                mySkillmanager = GameObject.Find("Player").GetComponent<SkillManager>();
            }
            return mySkillmanager;
        }

        set
        {
            mySkillmanager = value;
        }
    }
}
