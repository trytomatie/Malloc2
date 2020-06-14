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
    public enum Type
    {
        Weapon = 0,
        Accessoire = 1,
        None = 3
    }
    private Type type = Type.None;
    private string name = "Passive";
    private string description = "";
    private int imageId = 2;
    private float[] attributeNumberWeight = new float[6];

    public PassiveSkill()
    {
        attributeNumberWeight[0] = 0;
        attributeNumberWeight[1] = 0;
        attributeNumberWeight[2] = 20;
        attributeNumberWeight[3] = 40;
        attributeNumberWeight[4] = 100;
        attributeNumberWeight[5] = 140;
    }

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


    public void AddRandomAttribute(int currentFloor)
    {

        int i = UnityEngine.Random.Range(0, PassiveSkillAttribute.TotalWeight);
        PassiveSkillAttribute psa = new PassiveSkillAttribute_IncreaseHP(UnityEngine.Random.Range(10 * currentFloor, 50 * currentFloor));
        if(i > 0 && i < PassiveSkillAttribute.Weight(ref i,"PassiveSkillAttribute_IncreaseHP"))
        {
            psa = new PassiveSkillAttribute_IncreaseHP(UnityEngine.Random.Range(10 * currentFloor, 50 * currentFloor));
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreaseSP"))
        {
            psa = new PassiveSkillAttribute_IncreaseSP(UnityEngine.Random.Range(1 * currentFloor, 6 * currentFloor));
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreaseATK"))
        {
            psa = new PassiveSkillAttribute_IncreaseATK(UnityEngine.Random.Range(1 * currentFloor, 10 * currentFloor));
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreaseDEF"))
        {
            psa = new PassiveSkillAttribute_IncreaseDEF(UnityEngine.Random.Range(1 * currentFloor, 4 * currentFloor));
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_Scout"))
        {
            psa = new PassiveSkillAttribute_Scout();
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_RoadOfThorns"))
        {
            psa = new PassiveSkillAttribute_RoadOfThorns();
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_RollingThunder"))
        {
            psa = new PassiveSkillAttribute_RollingThunder();
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_Striker"))
        {
            psa = new PassiveSkillAttribute_Striker();
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_Rest"))
        {
            psa = new PassiveSkillAttribute_Rest(UnityEngine.Random.Range(20 * currentFloor, 50 * currentFloor));
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_Glass"))
        {
            psa = new PassiveSkillAttribute_Glass();
        }
        AddAtribute(psa);
    }

    public static PassiveSkill GenerateRandomPassive(int currentFloor,Statusmanager.CharacterClass cClass)
    {
        int rndInt = UnityEngine.Random.Range(1, Mathf.Clamp(currentFloor * 10, currentFloor, 100 + (currentFloor * 2)));
        PassiveSkill p = new PassiveSkill();
        p.Type1 = ((Type)UnityEngine.Random.Range(0, 2));
        if(p.Type1 == Type.Weapon)
        {
            p.Name = "Iron Sword";
            p.ImageId = 7;
        }
        else if(p.Type1 == Type.Accessoire)
        {
            p.Name = "Bracelet";
            p.ImageId = 8;
        }
        PassiveSkillAttribute.RestoreAttributeWeights();
        switch(cClass)
        {
            case Statusmanager.CharacterClass.Undefined:
                foreach (float weight in p.attributeNumberWeight)
                {
                    if (weight < rndInt)
                    {
                        p.AddRandomAttribute(currentFloor);
                    }
                }
                break;
            case Statusmanager.CharacterClass.Warrior:

                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseATK"] = 200;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseHP"] = 200;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseHP"] = 20;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_Striker"] = 20;

                p.attributeNumberWeight[2] = 0;
                p.attributeNumberWeight[3] = 10;


                p.AddAtribute(new PassiveSkillAttribute_IncreaseATK(UnityEngine.Random.Range(10 * currentFloor, 50 * currentFloor)));
                p.AddAtribute(new PassiveSkillAttribute_IncreaseHP(UnityEngine.Random.Range(10 * currentFloor, 50 * currentFloor)));
                int skip = 2;
                foreach (float weight in p.attributeNumberWeight)
                {
                    if(skip > 0)
                    {
                        skip--;
                        continue;
                    }
                    if (weight < rndInt)
                    {
                        p.AddRandomAttribute(currentFloor);
                    }
                }
                break;
            case Statusmanager.CharacterClass.Mage:
            
                foreach (float weight in p.attributeNumberWeight)
                {
                    if (weight < rndInt)
                    {
                        p.AddRandomAttribute(currentFloor);
                    }
                }
                break;
        }

        return p;
    }

    public void AddAtribute(PassiveSkillAttribute p)
    {
        passiveSkillAttributes.Add(p);
        Description += p.Name + "\n";
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


    public int ImageId
    {
        get
        {
            return imageId;
        }

        set
        {
            imageId = value;
        }
    }

    public Type Type1
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }
}
