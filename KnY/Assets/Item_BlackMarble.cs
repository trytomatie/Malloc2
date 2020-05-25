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
    public Item_BlackMarble()
    {
        this.artifactItem = false;
        this.itemId = 10;
        this.itemName = "Black Marble";
        this.description = "Mystierious item bestowed upon you by the architect of the labyrinth, it shall light you the way.";
    }


    public override void ApplyEffect(GameObject g)
    {
        myGameObject = GameObject.Instantiate(PublicGameResources.GetResource().blackMarble, g.transform.position,Quaternion.identity);
        myGameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = g.transform;
    }

    public override void RemoveEffect(GameObject g)
    {
        GameObject.Destroy(myGameObject);
    }

    public override void AddAditionalStack(GameObject g)
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
        Director.GetInstance().SetFadeMaterial(fadeStart, fadeEnd, myGameObject.GetComponent<BlackMarble>().interfaceMaterial);
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
}
