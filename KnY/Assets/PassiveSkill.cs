using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class PassiveSkill
{
    private List<PassiveSkillAttribute> passiveSkillAttributes = new List<PassiveSkillAttribute>();
    private string name = "Passive";
    private string description = "";


    public void ApplyEffects(GameObject source)
    {
        foreach(PassiveSkillAttribute a in PassiveSkillAttributes)
        {
            a.ApplyEffect(source);
        }
    }

    public void RemoveEffects(GameObject source)
    {
        foreach (PassiveSkillAttribute a in PassiveSkillAttributes)
        {
            a.RemoveEffect(source);
        }
    }


    public void AddRandomAttribute()
    {
        int i = UnityEngine.Random.Range(0, 6);
        PassiveSkillAttribute psa = new PassiveSkillAttribute_IncreaseHP(50);
        switch (i)
        {
            case 1:
                psa = new PassiveSkillAttribute_IncreaseHP(50);
                break;
            case 2:
                psa = new PassiveSkillAttribute_IncreaseSP(50);
                break;
            case 3:
                psa = new PassiveSkillAttribute_IncreaseATK(20);
                break;
            case 4:
                psa = new PassiveSkillAttribute_IncreaseDEF(10);
                break;
            case 5:
                psa = new PassiveSkillAttribute_Scout();
                break;
            case 6:
                psa = new PassiveSkillAttribute_RoadOfThorns();
                break;
        }
        Description += psa.Name + "\n";
        passiveSkillAttributes.Add(psa);
    }

    public static PassiveSkill GenerateRandomPassive()
    {
        PassiveSkill p = new PassiveSkill();
        p.AddRandomAttribute();
        p.AddRandomAttribute();
        return p;
    }


    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public List<PassiveSkillAttribute> PassiveSkillAttributes
    {
        get
        {
            return passiveSkillAttributes;
        }
    }
}
