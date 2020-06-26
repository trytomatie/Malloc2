using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class SkillGenerator
{
    private static Dictionary<String, int> baseSkillWeights;
    private static Dictionary<String, int> skillWeights = new Dictionary<String, int>()
    {
        { "Skill_BurnSlash", 100 },
        { "Skill_AoeDash", 100 },
        { "Skill_Cure", 80 },
        { "Skill_Guard", 50 },
        { "Skill_Erruption", 50 },
        { "Skill_Rampart", 90 },
        { "Skill_PoisonSting", 100 },
        { "Skill_Slam", 100 },
        { "Skill_Solatii", 50 },
        { "Skill_Slice", 100 },
        { "Skill_SummonReaver", 30 },
        { "Skill_SpreadingEruption",10 },
        { "Skill_SummonThunder",70 },
        { "Skill_SummonThySpiders",40 },
        { "Skill_LesserThunder",100 },
        { "Skill_HolyRod",100 },
        { "Skill_Smite",100 },
        { "Skill_Assize",100 },
        { "Skill_Drainage",100 },
        { "Skill_Slurp",70 },
        { "Skill_ElShamac",50 },
    };

    public static Skill GetRandomSkill(GameObject target)
     {
        RestoreAttributeWeights();
        Dictionary<String, int> skillWeightRef = skillWeights;
        Statusmanager.CharacterClass characterClass = target.GetComponent<Statusmanager>().characterClass;
        int rndVal = UnityEngine.Random.Range(0, 100);
        if(rndVal > 35)
        { 
            if (characterClass == Statusmanager.CharacterClass.Warrior)
            {
                skillWeightRef = GetEmptyWeightList();
                skillWeightRef["Skill_AoeDash"] = 100;
                skillWeightRef["Skill_Guard"] = 20;
                skillWeightRef["Skill_Rampart"] = 40;
                skillWeightRef["Skill_Slam"] = 80;
                skillWeightRef["Skill_Solatii"] = 5;
                skillWeightRef["Skill_Slice"] = 100;
                skillWeightRef["Skill_PoisonSting"] = 100;
            }
            if (characterClass == Statusmanager.CharacterClass.Mage)
            {
                skillWeightRef = GetEmptyWeightList();
                skillWeightRef["Skill_LesserThunder"] = 100;
                skillWeightRef["Skill_SummonThunder"] = 80;
                skillWeightRef["Skill_Erruption"] = 60;
                skillWeightRef["Skill_Cure"] = 60;
                skillWeightRef["Skill_Slam"] = 60;
                skillWeightRef["Skill_BurnSlash"] = 100;
                skillWeightRef["Skill_Slice"] = 20;
                skillWeightRef["Skill_Slurp"] = 80;
            }
            if (characterClass == Statusmanager.CharacterClass.Priest)
            {
                skillWeightRef = GetEmptyWeightList();
                skillWeightRef["Skill_SummonThunder"] = 10;
                skillWeightRef["Skill_Erruption"] = 5;
                skillWeightRef["Skill_HolyRod"] = 80;
                skillWeightRef["Skill_Cure"] = 80;
                skillWeightRef["Skill_Slam"] = 70;
                skillWeightRef["Skill_BurnSlash"] = 20;
                skillWeightRef["Skill_Smite"] = 80;
                skillWeightRef["Skill_Slurp"] = 40;
                skillWeightRef["Skill_Assize"] = 50;
            }
            if (characterClass == Statusmanager.CharacterClass.Summoner)
            {
                skillWeightRef = GetEmptyWeightList();
                skillWeightRef["Skill_SummonThunder"] = 40;
                skillWeightRef["Skill_Erruption"] = 5;
                skillWeightRef["Skill_SummonReaver"] = 100;
                skillWeightRef["Skill_SummonThySpiders"] = 70;
                skillWeightRef["Skill_BurnSlash"] = 20;
                skillWeightRef["Skill_ElShamac"] = 40;
            }
            if (characterClass == Statusmanager.CharacterClass.Paladin)
            {
                skillWeightRef = GetEmptyWeightList();
                skillWeightRef["Skill_AoeDash"] = 20;
                skillWeightRef["Skill_Guard"] = 120;
                skillWeightRef["Skill_Rampart"] = 120;
                skillWeightRef["Skill_Slam"] = 80;
                skillWeightRef["Skill_Solatii"] = 5;
                skillWeightRef["Skill_Slice"] = 20;
                skillWeightRef["Skill_PoisonSting"] = 10;
                skillWeightRef["Skill_Assize"] = 40;
            }
        }
        skillWeights = skillWeightRef;
        return PickSkill(target);
    }

    public static Skill PickSkill(GameObject target)
    {

        int i = UnityEngine.Random.Range(0, TotalWeight);
        Skill skill = new Skill_ThunderStrike(70, 0.8f, 0.25f, 3, false, target.GetComponent<SpriteRenderer>().material);

        if (i > 0 && Weight(ref i, "Skill_SummonThunder"))
        {
            skill = new Skill_ThunderStrike(15, 0.8f, 0.25f, 3, false, target.GetComponent<SpriteRenderer>().material);
        }
        else if (i > 0 && Weight(ref i, "Skill_BurnSlash"))
        {
            skill = new Skill_BurnSlash(8f, 0.2f, false);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_AoeDash"))
        {
            skill = new Skill_AoeDash(24f, 0.4f, false);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_Cure"))
        {
            skill = new Skill_Cure(50, 1.8f, false);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_Rampart"))
        {
            skill = new Skill_Rampart(30f, 0.1f, false);
        }
        else if (i > 0 && Weight(ref i, "Skill_PoisonSting"))
        {
            skill = new Skill_PoisonSting(8f, 0.2f, false);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_Slam"))
        {
            skill = new Skill_Slam(8, 0.1f, false);
        }
        else if (i > 0 &&Weight(ref i, "Skill_Guard"))
        {
            skill = new Skill_Guard(30f, 0.1f, false);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_SummonReaver"))
        {
            skill = new Skill_SummonReaver(50f, 1f, false);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_Solatii"))
        {
            skill = new Skill_Solatii(90, 2f, false);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_Slice"))
        {
            skill = new Skill_Slice(8, 0.1f, false);
        }
        else if (i > 0 && Weight(ref i, "Skill_Erruption"))
        {
            skill = new Skill_Erruption(55f, 0.4f, 0.05f, false);
        }
        else if (i > 0 && Weight(ref i, "Skill_SpreadingEruption"))
        {
            skill = new Skill_SpreadingEruption(70f, 0.4f, 0.05f, false);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_SummonThySpiders"))
        {
            skill = new Skill_SummonThySpiders(15, 1f, false);
        }
        else if (i > 0 && Weight(ref i, "Skill_LesserThunder"))
        {
            skill = new Skill_LesserThunder(8, 0.1f, false, target.GetComponent<SpriteRenderer>().material);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_HolyRod"))
        {
            skill = new Skill_HolyRod(8, 0.1f, false);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_Smite"))
        {
            skill = new Skill_Smite(12, 0.3f,8, false);
        }
        else if (i > 0 && Weight(ref i, "Skill_Assize"))
        {
            skill = new Skill_Assize(45, 0.7f, false);
        }
        else if (i > 0 &&Weight(ref i, "Skill_Drainage"))
        {
            skill = new Skill_Drainage(30, 0.6f, false);
        }
        else if (i > 0 &&  Weight(ref i, "Skill_Drainage"))
        {
            skill = new Skill_Slurp(60, 0.6f, false);
        }
        else if (i > 0 && Weight(ref i, "Skill_Drainage"))
        {
            skill = new Skill_ElShamac(70, 0.6f, false);
        }
        return skill;

    }
    public static int TotalWeight
    {
        get
        {
            int weight = 0;
            foreach (int w in skillWeights.Values)
            {
                weight += w;
            }
            return weight;
        }
    }

    public static Dictionary<String, int> AttributeWeights
    {
        get
        {
            if (baseSkillWeights == null)
            {
                baseSkillWeights = skillWeights.ToDictionary(entry => entry.Key, entry => entry.Value);
            }
            return skillWeights;
        }

        set
        {
            skillWeights = value;
        }
    }

    public static Dictionary<String, int> BaseattributeWeights
    {
        get
        {
            if (baseSkillWeights == null)
            {
                baseSkillWeights = skillWeights.ToDictionary(entry => entry.Key, entry => entry.Value);
            }
            return baseSkillWeights;
        }

        set
        {
            baseSkillWeights = value;
        }
    }

    public static void RestoreAttributeWeights()
    {
        AttributeWeights = BaseattributeWeights.ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public static Dictionary<String, int> GetEmptyWeightList()
    {
        Dictionary<String, int>  list = skillWeights.ToDictionary(entry => entry.Key, entry => entry.Value);
        foreach (String val in list.Keys.ToList())
        {
            list[val] = 0;
        }
        return (list);
    }

    public static bool Weight(ref int i, String value)
    {
        if(i < skillWeights[value])
        {
            return true;
        }
        i -= skillWeights[value];
        return false;
    }

}
