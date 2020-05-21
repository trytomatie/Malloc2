using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemId;
    public string itemName;
    public string description;
    public Sprite image;
    public int stacks = 1;
    public enum CommonItems
    {
        Item_TotemOfGrief = 1,
        Item_BandanaOfMight = 2,
        Item_Affectionate_Eminence = 3,
        Item_TitansKidney = 4,
        Item_BootsOfFlight = 7,
        Item_RedFairy = 9
    }
    public enum UncommonItems
    {
        Item_Blight = 5,
        Item_CorruptedAmethyst = 8
    }
    public enum RareItems
    {
        Item_BlueMoonStone = 6
    }
    public enum EpicItems
    {
        Item_BlueMoonStone = 6
    }


    public virtual void ApplyEffect(GameObject g)
    {

    }

    public virtual void RemoveEffect(GameObject g)
    {

    }

    public virtual void AddAditionalStack(GameObject g)
    {
        RemoveEffect(g);
        stacks++;
        ApplyEffect(g);
    }

    public virtual void Initialize()
    {

    }

    public static Item GenerateItem(int id)
    {
        Item item;
        switch (id)
        {
            case 1:
                item = new Item_TotemOfGrief();
                return item;
            case 2:
                item = new Item_BandanaOfMight();
                return item;
            case 3:
                item = new Item_Affectionate_Eminence();
                return item;
            case 4:
                item = new Item_TitansKidney();
                return item;
            case 5:
                item = new Item_Blight();
                return item;
            case 6:
                item = new Item_BlueMoonStone();
                return item;
            case 7:
                item = new Item_BootsOfFlight();
                return item;
            case 8:
                item = new Item_CorruptedAmethyst();
                return item;
            case 9:
                item = new Item_RedFairy();
                return item;
            default:
                return null;
        }
    }

    public static int GenerateRandomCommonItemID()
    {
        CommonItems id = CommonItems.Item_TotemOfGrief;
        System.Random rnd = new System.Random();
        Array values = Enum.GetValues(typeof(CommonItems));
        id = (CommonItems)values.GetValue(rnd.Next(values.Length));
        return (int)id;
    }

    public static int GenerateRandomUncommonItemID()
    {
        UncommonItems id = UncommonItems.Item_Blight;
        System.Random rnd = new System.Random();
        Array values = Enum.GetValues(typeof(UncommonItems));
        id = (UncommonItems)values.GetValue(rnd.Next(values.Length));
        return (int)id;
    }
    public static int GenerateRandomRareItemID()
    {
        RareItems id = RareItems.Item_BlueMoonStone;
        System.Random rnd = new System.Random();
        Array values = Enum.GetValues(typeof(RareItems));
        id = (RareItems)values.GetValue(rnd.Next(values.Length));
        return (int)id;
    }
    public static int GenerateRandomEpicItemID()
    {
        EpicItems id = EpicItems.Item_BlueMoonStone;
        System.Random rnd = new System.Random();
        Array values = Enum.GetValues(typeof(EpicItems));
        id = (EpicItems)values.GetValue(rnd.Next(values.Length));
        return (int)id;
    }


    public static Material GetItemMaterial(int id)
    {
        if (Enum.IsDefined(typeof(CommonItems), id))
        {
            return PublicGameResources.GetResource().itemMaterials[0];
        }
        if (Enum.IsDefined(typeof(UncommonItems), id))
        {
            return PublicGameResources.GetResource().itemMaterials[1];
        }
        if (Enum.IsDefined(typeof(RareItems), id))
        {
            return PublicGameResources.GetResource().itemMaterials[2];
        }
        return PublicGameResources.GetResource().itemMaterials[3];
    }

    public static Material GetItemDescriptionMaterial(int id)
    {
        Debug.Log(id);
        if (Enum.IsDefined(typeof(CommonItems), id))
        {
            return PublicGameResources.GetResource().itemDescriptionMaterials[0];
        }
        if (Enum.IsDefined(typeof(UncommonItems), id))
        {
            return PublicGameResources.GetResource().itemDescriptionMaterials[1];
        }
        if (Enum.IsDefined(typeof(RareItems), id))
        {
            return PublicGameResources.GetResource().itemDescriptionMaterials[2];
        }
        return PublicGameResources.GetResource().itemDescriptionMaterials[3];
    }

}
