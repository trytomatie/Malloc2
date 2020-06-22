using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActiveSkillExchangeManager : MonoBehaviour
{
    private static List<UI_ActiveSkillExchangeManager> instances = new List<UI_ActiveSkillExchangeManager>();
    public UI_SkillDisplay activeSkill1;
    public UI_SkillDisplay activeSkill2;
    public UI_SkillDisplay activeSkill3;
    public UI_SkillDisplay activeSkill4;
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
        activeSkill3.Skill = null;
        activeSkill4.Skill = null;
        newActiveSkill.Skill = null;
    }

    public static void OpenSkillExchangeWindow(Skill newSkill)
    {

        foreach (UI_ActiveSkillExchangeManager instance in Instances)
        {
            instance.ClearAllSlots();
            if (MySkillmanager.ActiveSkills[2] != null)
            {
                instance.activeSkill1.Skill = MySkillmanager.ActiveSkills[2];
            }
            else
            {
                instance.activeSkill1.Skill = null;
            }

            if (MySkillmanager.ActiveSkills[3] != null)
            {
                instance.activeSkill2.Skill = MySkillmanager.ActiveSkills[3];
            }
            else
            {
                instance.activeSkill2.Skill = null;
            }

            if (MySkillmanager.ActiveSkills[4] != null)
            {
                instance.activeSkill3.Skill = MySkillmanager.ActiveSkills[4];
            }
            else
            {
                instance.activeSkill3.Skill = null;
            }

            if (MySkillmanager.ActiveSkills[5] != null)
            {
                instance.activeSkill4.Skill = MySkillmanager.ActiveSkills[5];
            }
            else
            {
                instance.activeSkill4.Skill = null;
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
            MySkillmanager.RemoveActiveSkill(0);
            MySkillmanager.AddActiveSkill(instance.newActiveSkill.Skill,0);
            break;
        }
        CloseSkillExchagneWindow();
    }

    public static void ExchangeSecond()
    {
        foreach (UI_ActiveSkillExchangeManager instance in Instances)
        {
            MySkillmanager.RemoveActiveSkill(1);
            MySkillmanager.AddActiveSkill(instance.newActiveSkill.Skill,1);
            break;
        }
        CloseSkillExchagneWindow();
    }

    public static void ExchangeThrid()
    {
        foreach (UI_ActiveSkillExchangeManager instance in Instances)
        {
            MySkillmanager.RemoveActiveSkill(2);
            MySkillmanager.AddActiveSkill(instance.newActiveSkill.Skill, 2);
            break;
        }
        CloseSkillExchagneWindow();
    }

    public static void ExchangeFourth()
    {
        foreach (UI_ActiveSkillExchangeManager instance in Instances)
        {
            MySkillmanager.RemoveActiveSkill(3);
            MySkillmanager.AddActiveSkill(instance.newActiveSkill.Skill, 3);
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
