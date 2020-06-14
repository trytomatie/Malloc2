using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute
{
    private string name;
    public int bonusWeightAplications = 0;
    private static Dictionary<String, int> attributeWeights = new Dictionary<String, int>()
    {
        { "PassiveSkillAttribute_IncreaseATK", 100 },
        { "PassiveSkillAttribute_IncreaseDEF", 80 },
        { "PassiveSkillAttribute_IncreaseHP", 110 },
        { "PassiveSkillAttribute_IncreaseSP", 40 },
        { "PassiveSkillAttribute_RoadOfThorns", 10 },
        { "PassiveSkillAttribute_Scout", 20 },
        { "PassiveSkillAttribute_RollingThunder", 5 },
        { "PassiveSkillAttribute_Striker", 5 },
        { "PassiveSkillAttribute_Rest", 35 },
        { "PassiveSkillAttribute_Glass", 5 },
    };
    private static Dictionary<String, int> baseattributeWeights;

    public virtual void ApplyEffect(GameObject source)
    {

    }

    public virtual void RemoveEffect(GameObject source)
    {

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

    public static int TotalWeight
    {
        get
        {
            int weight = 0;
            foreach(int w in attributeWeights.Values)
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
            if(baseattributeWeights == null)
            {
                baseattributeWeights = attributeWeights.ToDictionary(entry => entry.Key,entry => entry.Value);
            }
            return attributeWeights;
        }

        set
        {
            attributeWeights = value;
        }
    }

    public static Dictionary<String, int> BaseattributeWeights
    {
        get
        {
            if (baseattributeWeights == null)
            {
                baseattributeWeights = attributeWeights.ToDictionary(entry => entry.Key, entry => entry.Value);
            }
            return baseattributeWeights;
        }

        set
        {
            baseattributeWeights = value;
        }
    }

    public static void RestoreAttributeWeights()
    {

        AttributeWeights = BaseattributeWeights.ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public static int Weight(ref int i, String value)
    {
        i -= attributeWeights[value];
        return attributeWeights[value];
    }
}
