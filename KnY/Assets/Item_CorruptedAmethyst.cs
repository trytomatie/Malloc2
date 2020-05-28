using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_CorruptedAmethyst : Item {

    public List<GameObject> companionList = new List<GameObject>();
    public Item_CorruptedAmethyst()
    {
        this.itemId = 8;
        this.attribute = "Dark";
        this.itemName = "Corrupted Amethyst";
        this.description = "Materialize a afflicted scavenger follower.";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }


    public override void ApplyEffect(GameObject g)
    {
        for(int i = 0; i < stacks;i++)
        {
            GameObject follower = GameObject.Instantiate(PublicGameResources.GetResource().corruptedAmethystFollower, g.transform.position, Quaternion.identity);
            follower.GetComponent<GenericFollowerAI>().followTarget = g;
            follower.GetComponent<Statusmanager>().faction = g.GetComponent<Statusmanager>().faction;
            companionList.Add(follower);
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        foreach(GameObject companion in companionList)
        {
            GameObject.Destroy(companion);
        }
        companionList.Clear();
    }

    public override void AddAditionalStack(GameObject g,Item otherItem)
    {
        base.AddAditionalStack(g, otherItem);
    }
}
