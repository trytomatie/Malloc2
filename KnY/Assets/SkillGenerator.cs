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
        { "Skill_SummonReaver", 60 },
        { "Skill_SpreadingEruption",10 },
        { "Skill_SummonThunder",80 },
        { "Skill_SummonThySpiders",40 }
    };

    public static Skill GetRandomSkill(GameObject target)
     {
        RestoreAttributeWeights();
        Dictionary<String, int> skillWeightRef = skillWeights;
        Statusmanager.CharacterClass characterClass = target.GetComponent<Statusmanager>().characterClass;
        int rndVal = UnityEngine.Random.Range(0, 100);
        if(rndVal > 30)
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
                skillWeightRef["Skill_SummonThunder"] = 100;
                skillWeightRef["Skill_Erruption"] = 40;
                skillWeightRef["Skill_Cure"] = 40;
                skillWeightRef["Skill_Slam"] = 60;
                skillWeightRef["Skill_BurnSlash"] = 100;
                skillWeightRef["Skill_Slice"] = 20;
            }
            if (characterClass == Statusmanager.CharacterClass.Priest)
            {
                skillWeightRef = GetEmptyWeightList();
                skillWeightRef["Skill_SummonThunder"] = 40;
                skillWeightRef["Skill_Erruption"] = 5;
                skillWeightRef["Skill_Cure"] = 100;
                skillWeightRef["Skill_Slam"] = 70;
                skillWeightRef["Skill_BurnSlash"] = 20;
            }
            if (characterClass == Statusmanager.CharacterClass.Summoner)
            {
                skillWeightRef = GetEmptyWeightList();
                skillWeightRef["Skill_SummonThunder"] = 40;
                skillWeightRef["Skill_Erruption"] = 5;
                skillWeightRef["Skill_SummonReaver"] = 100;
                skillWeightRef["Skill_SummonThySpiders"] = 70;
                skillWeightRef["Skill_BurnSlash"] = 20;
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
            }
        }
        skillWeights = skillWeightRef;
        return PickSkill(target);
    }

    public static Skill PickSkill(GameObject target)
    {

        int i = UnityEngine.Random.Range(0, PassiveSkillAttribute.TotalWeight);
        Skill skill = new Skill_ThunderStrike(10, 0.8f, 0.25f, 3, false, target.GetComponent<SpriteRenderer>().material);
        if (i > 0 && i <Weight(ref i, "Skill_SummonThunder"))
        {
            skill = new Skill_ThunderStrike(10, 0.8f, 0.25f, 3, false, target.GetComponent<SpriteRenderer>().material);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_BurnSlash"))
        {
            skill = new Skill_BurnSlash(8f, 0.2f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_AoeDash"))
        {
            skill = new Skill_AoeDash(10f, 0.4f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_Cure"))
        {
            skill = new Skill_Cure(20, 1.8f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_Rampart"))
        {
            skill = new Skill_Rampart(30f, 0.1f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_PoisonSting"))
        {
            skill = new Skill_PoisonSting(8f, 0.2f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_Slam"))
        {
            skill = new Skill_Slam(6, 0.1f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_Guard"))
        {
            skill = new Skill_Guard(30f, 0.1f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_SummonReaver"))
        {
            skill = new Skill_SummonReaver(20f, 1f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_Solatii"))
        {
            skill = new Skill_Solatii(30, 2f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_Slice"))
        {
            skill = new Skill_Slice(6, 0.1f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_Erruption"))
        {
            skill = new Skill_Erruption(15f, 0.4f, 0.05f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_SpreadingEruption"))
        {
            skill = new Skill_SpreadingEruption(23f, 0.4f, 0.05f, false);
        }
        else if (i > 0 && i < Weight(ref i, "Skill_SummonThySpiders"))
        {
            skill = new Skill_SummonThySpiders(15, 1f, false);
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

    public static int Weight(ref int i, String value)
    {
        i -= skillWeights[value];
        return skillWeights[value];
    }

}
