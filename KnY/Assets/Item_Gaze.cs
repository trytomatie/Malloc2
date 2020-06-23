using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Gaze : Item {

    public List<GameObject> companionList = new List<GameObject>();
    public Item_Gaze()
    {
        this.itemId = 35;
        this.attribute = "Dark";
        this.itemName = "Gaze";
        this.description = "Materialize an indolent eye follower.";
        this.detailedDescription = "Materialize an indolent eye follower per stack.";
        this.series.Add(ItemSeries.Series.Scout);
        this.series.Add(ItemSeries.Series.Minionmancer);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }


    public override void ApplyEffect(GameObject g)
    {
        for(int i = 0; i < g.GetComponent<Statusmanager>().level;i++)
        {
            GameObject follower = GameObject.Instantiate(PublicGameResources.GetResource().gazeFollower, g.transform.position, Quaternion.identity);
            follower.GetComponent<AI_EyeFollower>().followTarget = g;
            follower.GetComponent<Statusmanager>().faction = g.GetComponent<Statusmanager>().faction;
            follower.GetComponent<Statusmanager>().level = g.GetComponent<Statusmanager>().level;
            follower.GetComponent<AIPath>().endReachedDistance += 0.16f * i;
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
