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
        attributeNumberWeight[2] = 60;
        attributeNumberWeight[3] = 90;
        attributeNumberWeight[4] = 105;
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


    public void AddRandomAttribute(int level)
    {
        int i = UnityEngine.Random.Range(0, PassiveSkillAttribute.TotalWeight);
        PassiveSkillAttribute psa = new PassiveSkillAttribute_IncreaseHP(UnityEngine.Random.Range(10 * level, 50 * level));
        if(i > 0 && i < PassiveSkillAttribute.Weight(ref i,"PassiveSkillAttribute_IncreaseHP"))
        {

            int baseValue = 100 + level * 12;
            psa = new PassiveSkillAttribute_IncreaseHP((int)UnityEngine.Random.Range(baseValue,baseValue*1.1f));
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseHP"] /= 2;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreaseSP"))
        {
            int baseValue = 10 + level * 6;
            psa = new PassiveSkillAttribute_IncreaseSP((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f));
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseSP"] /= 2;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreaseATK"))
        {
            int baseValue = 10 + level * 8;
            psa = new PassiveSkillAttribute_IncreaseATK((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f));
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseATK"] /= 2;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreaseDEF"))
        {
            int baseValue = 16 + level * 4;
            psa = new PassiveSkillAttribute_IncreaseDEF((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f));
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseDEF"] /= 2;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_Scout"))
        {
            psa = new PassiveSkillAttribute_Scout();
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_Scout"] = 0;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_RoadOfThorns"))
        {
            psa = new PassiveSkillAttribute_RoadOfThorns();
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_RoadOfThorns"] = 0;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_RollingThunder"))
        {
            psa = new PassiveSkillAttribute_RollingThunder();
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_RollingThunder"] = 0;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_Striker"))
        {
            psa = new PassiveSkillAttribute_Striker();
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_Striker"] = 0;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_Rest"))
        {
            int baseValue = 100 + level * 12;
            psa = new PassiveSkillAttribute_Rest((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f));
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_Rest"] = 0;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_Glass"))
        {
            psa = new PassiveSkillAttribute_Glass();
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_Glass"] = 0;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreaseMagicAttack"))
        {
            int baseValue = 10 + level * 9;
            psa = new PassiveSkillAttribute_IncreaseMagicAttack((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f));
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseMagicAttack"] /= 2;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreaseSTR"))
        {
            int baseValue = 10 + level * 10;
            psa = new PassiveSkillAttribute_IncreaseSTR((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f));
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseSTR"] /= 2;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreaseINT"))
        {
            int baseValue = 10 + level * 10;
            psa = new PassiveSkillAttribute_IncreaseINT((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f));
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseINT"] /= 2;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreaseDEX"))
        {
            int baseValue = 10 + level * 11;
            psa = new PassiveSkillAttribute_IncreaseDEX((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f));
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseDEX"] /= 2;
        }
        else if (i > 0 && i < PassiveSkillAttribute.Weight(ref i, "PassiveSkillAttribute_IncreasePIE"))
        {
            int baseValue = 10 + level * 11;
            psa = new PassiveSkillAttribute_IncreasePIE((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f));
            PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreasePIE"] /= 2;
        }
        AddAtribute(psa);
    }

    public static PassiveSkill GenerateRandomPassive(int level,Statusmanager.CharacterClass cClass)
    {
        int rarity = UnityEngine.Random.Range(0, 101);
        int bonus = 0;
        string color = "<color=white>";
        if (rarity < 80)
        {
            bonus = 0;
            color = "<color=white>";
        }
        else if (rarity < 90)
        {
            bonus = 2;
            color = "<color=green>";
        }
        else if (rarity < 98)
        {
            bonus = 4;
            color = "<color=blue>";
        }
        else if (rarity < 100)
        {
            bonus = 7;
            color = "<color=purple>";
        }
        else if (rarity < 101)
        {
            bonus = 9;
            color = "<color=orange>";
        }
        level += bonus;
        int rndInt = UnityEngine.Random.Range(1, Mathf.Clamp((level+7) * 10, level, 100 + (level * 2)));
        PassiveSkill p = new PassiveSkill();
        p.Type1 = ((Type)UnityEngine.Random.Range(0, 2));
        if(p.Type1 == Type.Weapon)
        {
            p.Name = color+ "Iron Sword</color>";
            p.ImageId = 7;
        }
        else if(p.Type1 == Type.Accessoire)
        {
            p.Name = color+"Bracelet</color>";
            p.ImageId = 8;
        }
        int skip = 0;
        int baseValue = 0;
        PassiveSkillAttribute.RestoreAttributeWeights();
        switch(cClass)
        {
            case Statusmanager.CharacterClass.Undefined:
                foreach (float weight in p.attributeNumberWeight)
                {
                    if (weight < rndInt)
                    {
                        p.AddRandomAttribute(level);
                    }
                }
                break;
            case Statusmanager.CharacterClass.Warrior:

                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseATK"] = 120;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseSTR"] = 120;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseHP"] = 120;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseSP"] = 20;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseINT"] = 20;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_Striker"] = 20;

                p.attributeNumberWeight[2] = 0;
                p.attributeNumberWeight[3] = 10;

                baseValue = 10 + level * 10;
                p.AddAtribute(new PassiveSkillAttribute_IncreaseSTR((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f)));
                baseValue = 50 + level * 12;
                p.AddAtribute(new PassiveSkillAttribute_IncreaseHP((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f)));
                skip = 2;
                foreach (float weight in p.attributeNumberWeight)
                {
                    if(skip > 0)
                    {
                        skip--;
                        continue;
                    }
                    if (weight < rndInt)
                    {
                        p.AddRandomAttribute(level);
                    }
                }
                break;
            case Statusmanager.CharacterClass.Mage:
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseATK"] = 60;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseINT"] = 150;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreasePIE"] = 60;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseSP"] = 120;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseMagicAttack"] = 100;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_Glass"] = 20;
                baseValue = 10 + level * 10;
                p.AddAtribute(new PassiveSkillAttribute_IncreaseINT((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f)));
                skip = 1;
                foreach (float weight in p.attributeNumberWeight)
                {
                    if (skip > 0)
                    {
                        skip--;
                        continue;
                    }
                    if (weight < rndInt)
                    {
                        p.AddRandomAttribute(level);
                    }
                }
                break;
            case Statusmanager.CharacterClass.Priest:
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseATK"] = 40;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreasePIE"] = 150;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseINT"] = 30;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseSP"] = 100;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseHP"] = 120;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseMagicAttack"] = 70;
                baseValue = 10 + level * 11;
                p.AddAtribute(new PassiveSkillAttribute_IncreasePIE((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f)));
                skip = 1;
                foreach (float weight in p.attributeNumberWeight)
                {
                    if (skip > 0)
                    {
                        skip--;
                        continue;
                    }
                    if (weight < rndInt)
                    {
                        p.AddRandomAttribute(level);
                    }
                }
                break;
            case Statusmanager.CharacterClass.Summoner:
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_MinionRest"] = 100;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseATK"] = 40;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseMagicAttack"] = 40;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseHP"] = 190;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_Scout"] = 40;
                p.AddAtribute(new PassiveSkillAttribute_MinionRest(UnityEngine.Random.Range(100 + 60 * level, 100 + 70 * level)));
                skip = 1;
                foreach (float weight in p.attributeNumberWeight)
                {
                    if (skip > 0)
                    {
                        skip--;
                        continue;
                    }
                    if (weight < rndInt)
                    {
                        p.AddRandomAttribute(level);
                    }
                }
                break;
            case Statusmanager.CharacterClass.Paladin:
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseATK"] = 40;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseSTR"] = 100;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreasePIE"] = 100;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseMagicAttack"] = 40;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseHP"] = 250;
                PassiveSkillAttribute.AttributeWeights["PassiveSkillAttribute_IncreaseDEF"] = 250;
                baseValue = 10 + level * 8;
                p.AddAtribute(new PassiveSkillAttribute_IncreaseDEF((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f)));
                baseValue = 50 + level * 12;
                p.AddAtribute(new PassiveSkillAttribute_IncreaseHP((int)UnityEngine.Random.Range(baseValue, baseValue * 1.1f)));
                skip = 2;
                foreach (float weight in p.attributeNumberWeight)
                {
                    if (skip > 0)
                    {
                        skip--;
                        continue;
                    }
                    if (weight < rndInt)
                    {
                        p.AddRandomAttribute(level);
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
