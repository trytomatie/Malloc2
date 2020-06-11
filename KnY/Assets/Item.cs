using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public int itemId;
    public string itemName;
    public string description;
    public string detailedDescription;
    public Sprite image;
    public int stacks = 1;
    public bool artifactItem = true;
    public string attribute = "None";
    public Inventory owner = null;
    public int position = 0;
    public float effectMutliplier = 1;
    public List<ItemSeries.Series> series = new List<ItemSeries.Series>();

    private static System.Random rnd = new System.Random();
    public enum CommonItems
    {
        Item_TitansKidney = 4,
        Item_BootsOfFlight = 7,
        Item_RedFairy = 9,
        Item_SpeedCrystal = 12,
        Item_DexterityCrystal = 13,
        Item_Blight = 5,
        Item_CorruptedAmethyst = 8,
        Item_WitchsHeart = 21,
        Item_DivineDaggers = 23,
        Item_DarkFairy = 26,
        Item_CompassionateProminence = 28,
        Item_VaingloriousAuthority = 34,
    }
    public enum UncommonItems
    {
        Item_Affectionate_Eminence = 3,
        Item_TotemOfGrief = 1,
        Item_MightCrystal = 11,
        Item_Affliction = 20,
        Item_InheritedArmor = 22,
        Item_DivineClockwork = 24,
        Item_RejuvinationFairy = 29,
        Item_Gaze = 35
    }
    public enum RareItems
    {
        Item_DivineWaters = 25,
        Item_BlueMoonStone = 6,
        Item_DesireableGreatness = 27,
        Item_ManaSprite = 31,

            
    }
    public enum EpicItems
    {
        Item_BigChonkerFairy = 32
    }

    public enum LegendaryItems
    {
        Item_OhLawdHeComin = 33
    }
    public enum UniqueItems
    {
        Item_BlackMarble = 10,
        Item_TheProcer = 30
    }
    public enum OtherItems
    {
        Item_LightCoin = 14,
        Item_DarkCoin = 15,
        Item_FireCoin = 16,
        Item_EarthCoin = 17,
        Item_WaterCoin = 18,
        Item_WindCoin = 19
    }
    public enum RemovedItems
    {
        Item_BandanaOfMight = 2
    }


    public virtual void ApplyEffect(GameObject g)
    {

    }

    public virtual void RemoveEffect(GameObject g)
    {

    }

    public virtual void AddAditionalStack(GameObject g,Item otherItem)
    {
        RemoveEffect(g);
        stacks+= otherItem.stacks;
        ApplyEffect(g);
    }

    public virtual void RefreshEffect(GameObject g)
    {
        RemoveEffect(g);
        ApplyEffect(g);
    }

    public virtual void InstanciateContextMenu()
    {
        ClearContextMenuItems();
        if(owner.GetComponent<PlayerController>() != null)
        { 
            UnityAction action = new UnityAction(DestroyItem);
            CreateContextMenuItem("Disenchant Artifact", action);
        }
    }

    public static void ClearContextMenuItems()
    {
        Transform contextMenu = GameObject.Find("ItemContextMenu").transform;
        int offensichlicherChildcount = contextMenu.childCount;
        for (int i = 0; i < offensichlicherChildcount; i++)
        {
            GameObject gj = contextMenu.GetChild(0).gameObject;
            gj.transform.SetParent(null,false);
            GameObject.Destroy(gj);
        }
    }


    public void DestroyItem()
    {
        DisposeContextMenu();
        owner.GetComponent<Inventory>().AddItem(GenerateTokens(this));
        owner.GetComponent<Inventory>().RemoveItem(this);
    }

    public static void CreateContextMenuItem(String itemText, UnityAction onClick)
    {
        GameObject contextMenu = GameObject.Find("ItemContextMenu");
        GameObject firstItem = GameObject.Instantiate(PublicGameResources.GetResource().itemContextMenuItem, Vector3.zero, Quaternion.identity, contextMenu.transform);
        firstItem.GetComponent<RectTransform>().localPosition = new Vector3(0, 0 - 40 * contextMenu.transform.childCount, 0);
        firstItem.GetComponent<Button>().onClick.AddListener(onClick);
        firstItem.transform.GetChild(0).GetComponent<Text>().text = itemText;
    }

    public static void DisposeContextMenu()
    {
        GameObject.Find("ItemContextMenu").transform.position = new Vector3(100000, 10000, 100000);
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
            case 10:
                item = new Item_BlackMarble();
                return item;
            case 11:
                item = new Item_MightCrystal();
                return item;
            case 12:
                item = new Item_SpeedCrystal();
                return item;
            case 13:
                item = new Item_DexterityCrystal();
                return item;
            case 14:
                item = new Item_LightCoin();
                return item;
            case 15:
                item = new Item_DarkCoin();
                return item;
            case 16:
                item = new Item_FireCoin();
                return item;
            case 17:
                item = new Item_EarthCoin();
                return item;
            case 18:
                item = new Item_WaterCoin();
                return item;
            case 19:
                item = new Item_WindCoin();
                return item;
            case 20:
                item = new Item_Affliction();
                return item;
            case 21:
                item = new Item_WitchsHeart();
                return item;
            case 22:
                item = new Item_InheritedArmor();
                return item;
            case 23:
                item = new Item_DivineDaggers();
                return item;
            case 24:
                item = new Item_DivineClockwork();
                return item;
            case 25:
                item = new Item_DivineWaters();
                return item;
            case 26:
                item = new Item_DarkFairy();
                return item;
            case 27:
                item = new Item_DesireableGreatness();
                return item;
            case 28:
                item = new Item_CompassionateProminence();
                return item;
            case 29:
                item = new Item_RejuvinationFairy();
                return item;
            case 30:
                item = new Item_TheProcer();
                return item;
            case 31:
                item = new Item_ManaSprite();
                return item;
            case 32:
                item = new Item_BigChonkerFairy();
                return item;
            case 33:
                item = new Item_OhLawdHeComin();
                return item;
            case 34:
                item = new Item_VaingloriousAuthority();
                return item;
            case 35:
                item = new Item_Gaze();
                return item;
            default:
                return null;
        }
    }

    public static Item GenerateTokens(Item item)
    {
        Item coin = null;
        int stackSize = 1;
        if (Enum.IsDefined(typeof(CommonItems), item.itemId))
        {
            stackSize = 3;
        }
        if (Enum.IsDefined(typeof(UncommonItems), item.itemId))
        {
            stackSize = 9;
        }
        if (Enum.IsDefined(typeof(RareItems), item.itemId))
        {
            stackSize = 27;
        }
        if (Enum.IsDefined(typeof(EpicItems), item.itemId))
        {
            stackSize = 42;
        }
        if (Enum.IsDefined(typeof(LegendaryItems), item.itemId))
        {
            stackSize = 81;
        }
        stackSize *= item.stacks;
        switch(item.attribute)
        {
            case "Light":
                coin = new Item_LightCoin();
                coin.stacks = stackSize;
                return coin;
            case "Dark":
                coin = new Item_DarkCoin();
                coin.stacks = stackSize;
                return coin;
            case "Fire":
                coin = new Item_FireCoin();
                coin.stacks = stackSize;
                return coin;
            case "Earth":
                coin = new Item_EarthCoin();
                coin.stacks = stackSize;
                return coin;
            case "Water":
                coin = new Item_WaterCoin();
                coin.stacks = stackSize;
                return coin;
            case "Wind":
                coin = new Item_WindCoin();
                coin.stacks = stackSize;
                return coin;
            default:
                return coin;
        }
    }

    public static int GenerateRandomCommonItemID()
    {
        CommonItems id = CommonItems.Item_Blight;
        Array values = Enum.GetValues(typeof(CommonItems));
        id = (CommonItems)values.GetValue(rnd.Next(values.Length));
        return (int)id;
    }

    public static int GenerateRandomUncommonItemID()
    {
        UncommonItems id = UncommonItems.Item_MightCrystal;
        Array values = Enum.GetValues(typeof(UncommonItems));
        id = (UncommonItems)values.GetValue(rnd.Next(values.Length));
        return (int)id;
    }
    public static int GenerateRandomRareItemID()
    {
        RareItems id = RareItems.Item_BlueMoonStone;
        Array values = Enum.GetValues(typeof(RareItems));
        int rndVal = rnd.Next(values.Length);
        id = (RareItems)values.GetValue(rndVal);
        return (int)id;
    }
    public static int GenerateRandomEpicItemID()
    {
        EpicItems id = EpicItems.Item_BigChonkerFairy;
        Array values = Enum.GetValues(typeof(EpicItems));
        id = (EpicItems)values.GetValue(rnd.Next(values.Length));
        return (int)id;
    }

    public static int GenerateRandomLegendaryItemID()
    {
        LegendaryItems id = LegendaryItems.Item_OhLawdHeComin;
        Array values = Enum.GetValues(typeof(LegendaryItems));
        id = (LegendaryItems)values.GetValue(rnd.Next(values.Length));
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
        if (Enum.IsDefined(typeof(EpicItems), id))
        {
            return PublicGameResources.GetResource().itemMaterials[3];
        }
        if (Enum.IsDefined(typeof(LegendaryItems), id))
        {
            return PublicGameResources.GetResource().itemMaterials[4];
        }
        return PublicGameResources.GetResource().itemMaterials[0];
    }

    public static Material GetItemDescriptionMaterial(int id)
    {
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
        if (Enum.IsDefined(typeof(EpicItems), id))
        {
            return PublicGameResources.GetResource().itemDescriptionMaterials[3];
        }
        if (Enum.IsDefined(typeof(LegendaryItems), id))
        {
            return PublicGameResources.GetResource().itemDescriptionMaterials[4];
        }
        return PublicGameResources.GetResource().itemDescriptionMaterials[0];
    }

    public static int GetItemCost(int id)
    {
        if (Enum.IsDefined(typeof(CommonItems), id))
        {
            return 4;
        }
        if (Enum.IsDefined(typeof(UncommonItems), id))
        {
            return 12;
        }
        if (Enum.IsDefined(typeof(RareItems), id))
        {
            return 100;
        }
        if (Enum.IsDefined(typeof(EpicItems), id))
        {
            return 200;
        }
        if (Enum.IsDefined(typeof(LegendaryItems), id))
        {
            return 200;
        }
        return 0;
    }

}
