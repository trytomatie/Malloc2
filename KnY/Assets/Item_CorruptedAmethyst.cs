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
        this.description = "Materialize an afflicted scavenger follower.";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Minionmancer);
    }


    public override void ApplyEffect(GameObject g)
    {
        for(int i = 0; i < stacks;i++)
        {
            GameObject follower = GameObject.Instantiate(PublicGameResources.GetResource().corruptedAmethystFollower, g.transform.position, Quaternion.identity);
            follower.GetComponent<AI_GenericFollower>().followTarget = g;
            follower.GetComponent<Statusmanager>().faction = g.GetComponent<Statusmanager>().faction;
            follower.GetComponent<Statusmanager>().level = g.GetComponent<Statusmanager>().level;
            companionList.Add(follower);
            g.GetComponent<Statusmanager>().AddFollower(follower);
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        foreach(GameObject companion in companionList)
        {

            g.GetComponent<Statusmanager>().RemoveFollwer(g);
            GameObject.Destroy(companion);
        }
        companionList.Clear();
    }

    public override void AddAditionalStack(GameObject g,Item otherItem)
    {
        base.AddAditionalStack(g, otherItem);
    }
}
