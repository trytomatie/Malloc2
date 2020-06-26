using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_BlackMarble : Item {

    public float attackSpeedBonus = 20f;
    public GameObject myGameObject;
    private bool isVisible = true;
    private bool searchForCommon = true;
    private bool searchForUncommon = false;
    private bool searchForBoss = true;
    public Item_BlackMarble()
    {
        this.artifactItem = false;
        this.itemId = 10;
        this.attribute = "Earth";
        this.itemName = "Black Marble";
        this.description = "Mystierious item bestowed upon you by the architect of the labyrinth, it shall light you the way.";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }


    public override void ApplyEffect(GameObject g)
    {
        myGameObject = GameObject.Instantiate(PublicGameResources.GetResource().blackMarble, g.transform.position,Quaternion.identity);
        myGameObject.GetComponent<BlackMarble>().itemRef = this;
        myGameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = g.transform;
    }

    public override void RemoveEffect(GameObject g)
    {
        GameObject.Destroy(myGameObject);
    }

    public override void AddAditionalStack(GameObject g,Item otherItem)
    {

    }

    public override void InstanciateContextMenu()
    {
        Debug.Log("Create Menu!");
        ClearContextMenuItems();
        string text1 = "Actiate Visible Mode";
        if(isVisible)
        {
            text1 = "Activate Invisible Mode";
        }
        CreateContextMenuItem(text1, new UnityAction(SetVisibleMode));
        if(GameObject.FindObjectOfType<MapGenerator>())
        { 
            string text2 = "Search for common treasureroom ";
            if (searchForCommon)
            {
                text2 = "Search for common treasureroom (Active)";
            }
            CreateContextMenuItem(text2, new UnityAction(SearchForCommontreasureRoom));
            string text3 = "Search for uncommon treasureroom ";
            if (searchForUncommon)
            {
                text3 = "Search for uncommon treasureroom(Active)";
            }
            CreateContextMenuItem(text3, new UnityAction(SearchForUncommontreasureRoom));
        }
        string text4 = "Locate Boss";
        if (searchForBoss)
        {
            text4 = "Locate Boss (Active)";
        }
        CreateContextMenuItem(text4, new UnityAction(LocateBoss));

    }

    private void SetVisibleMode()
    {
        DisposeContextMenu();
        isVisible = !isVisible;
        int fadeStart = 0;
        int fadeEnd = 0;
        if(isVisible)
        {
            fadeStart = 0;
            fadeEnd = 1;
        }
        else
        {
            fadeStart = 1;
            fadeEnd = 0;
        }
        Director.GetInstance().SetFadeMaterial(fadeStart, fadeEnd, myGameObject.GetComponent<BlackMarble>().interfaceMaterial,1);
    }

    private void SearchForCommontreasureRoom()
    {
        DisposeContextMenu();
        BlackMarble bm = myGameObject.GetComponent<BlackMarble>();
        searchForCommon = !searchForCommon;
        if (searchForCommon)
        {
            bm.SearchAllCommonTreasureChunks();
        }
        else
        {
            bm.ClearChunkIdsThatAreSearched(bm.commontreasureChunkIds);
        }
        if (searchForUncommon && searchForCommon)
        {
            bm.SearchAllCommonAndUncommonTreasureChunks();
        }

    }

    private void SearchForUncommontreasureRoom()
    {
        DisposeContextMenu();
        BlackMarble bm = myGameObject.GetComponent<BlackMarble>();
        searchForUncommon = !searchForUncommon;
        if (searchForUncommon)
        {
            bm.SearchAllUncommonTreasureChunks();
        }
        else
        {
            bm.ClearChunkIdsThatAreSearched(bm.uncommontreasureChunkIds);
        }
        if (searchForUncommon && searchForCommon)
        {
            bm.SearchAllCommonAndUncommonTreasureChunks();
        }
    }

    private void LocateBoss()
    {
        searchForBoss = !searchForBoss;
        DisposeContextMenu();
        BlackMarble bm = myGameObject.GetComponent<BlackMarble>();
        bm.canLocateBosses = searchForBoss;
    }
}
