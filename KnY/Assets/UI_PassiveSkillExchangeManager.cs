using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PassiveSkillExchangeManager : MonoBehaviour
{
    private static List<UI_PassiveSkillExchangeManager> instances = new List<UI_PassiveSkillExchangeManager>();
    public UI_PassiveSkillDisplay passiveSkill1;
    public UI_PassiveSkillDisplay passiveSkill2;
    public UI_PassiveSkillDisplay newPassiveSkill;
    private static SkillManager mySkillmanager;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ClearAllSlots()
    {
        passiveSkill1.skill = null;
        passiveSkill2.skill = null;
        newPassiveSkill.skill = null;
    }

    public static void OpenSkillExchangeWindow(PassiveSkill newSkill)
    {

        foreach (UI_PassiveSkillExchangeManager instance in Instances)
        {
            instance.ClearAllSlots();
            if (MySkillmanager.passiveSkills.Count > 0)
            { 
            instance.passiveSkill1.skill = MySkillmanager.passiveSkills[0];
            }
            else
            {
                instance.passiveSkill1.skill = null;
            }
            if (MySkillmanager.passiveSkills.Count > 1)
            {
                instance.passiveSkill2.skill = MySkillmanager.passiveSkills[1];
            }
            else
            {
                instance.passiveSkill2.skill = null;
            }
            instance.newPassiveSkill.skill = newSkill;
            instance.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public static void ExchangeFirst()
    {
        foreach (UI_PassiveSkillExchangeManager instance in Instances)
        {
            MySkillmanager.RemovePassiveSkill(instance.passiveSkill1.skill);
            MySkillmanager.AddPassiveSkill(instance.newPassiveSkill.skill);
            break;
        }
        CloseSkillExchagneWindow();
    }

    public static void ExchangeSecond()
    {
        foreach (UI_PassiveSkillExchangeManager instance in Instances)
        {
            MySkillmanager.RemovePassiveSkill(instance.passiveSkill2.skill);
            MySkillmanager.AddPassiveSkill(instance.newPassiveSkill.skill);
            break;
        }
        CloseSkillExchagneWindow();
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
