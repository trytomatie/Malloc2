using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActiveSkillExchangeManager : MonoBehaviour
{
    private static List<UI_ActiveSkillExchangeManager> instances = new List<UI_ActiveSkillExchangeManager>();
    public UI_SkillDisplay activeSkill1;
    public UI_SkillDisplay activeSkill2;
    public UI_SkillDisplay newActiveSkill;
    private static SkillManager mySkillmanager;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ClearAllSlots()
    {
        activeSkill1.Skill = null;
        activeSkill2.Skill = null;
        newActiveSkill.Skill = null;
    }

    public static void OpenSkillExchangeWindow(Skill newSkill)
    {

        foreach (UI_ActiveSkillExchangeManager instance in Instances)
        {
            instance.ClearAllSlots();
            if (MySkillmanager.activeSkills.Count > 2)
            { 
            instance.activeSkill1.Skill = MySkillmanager.activeSkills[2];
            }
            else
            {
                instance.activeSkill1.Skill = null;
            }
            if (MySkillmanager.activeSkills.Count > 3)
            {
                instance.activeSkill2.Skill = MySkillmanager.activeSkills[3];
            }
            else
            {
                instance.activeSkill2.Skill = null;
            }
            instance.newActiveSkill.Skill = newSkill;
            instance.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public static void ExchangeFirst()
    {
        foreach (UI_ActiveSkillExchangeManager instance in Instances)
        {
            MySkillmanager.RemoveActiveSkill(instance.activeSkill1.Skill);
            MySkillmanager.AddActiveSkill(instance.newActiveSkill.Skill);
            break;
        }
        CloseSkillExchagneWindow();
    }

    public static void ExchangeSecond()
    {
        foreach (UI_ActiveSkillExchangeManager instance in Instances)
        {
            MySkillmanager.RemoveActiveSkill(instance.activeSkill2.Skill);
            MySkillmanager.AddActiveSkill(instance.newActiveSkill.Skill);
            break;
        }
        CloseSkillExchagneWindow();
    }

    public static void CloseSkillExchagneWindow()
    {
        foreach (UI_ActiveSkillExchangeManager instance in Instances)
        {
            instance.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public static List<UI_ActiveSkillExchangeManager> Instances
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
            foreach(UI_ActiveSkillExchangeManager instance in Instances)
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
