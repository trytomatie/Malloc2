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

    public Color32 commonGlow;
    public Color32 unCommonGlow;
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
        isVisible = !isVisible;
        myGameObject.GetComponent<SpriteRenderer>().enabled = isVisible;
    }

    private void SearchForCommontreasureRoom()
    {
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
