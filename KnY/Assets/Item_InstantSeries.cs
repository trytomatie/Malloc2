using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_InstantSeries : Item {

    public Item_InstantSeries()
    {
        this.itemId = 36;
        this.attribute = "Instant";
        this.itemName = "Instant Series";
        this.description = "Gives you 2 Random Series";
        this.detailedDescription = "Gives you 2 Random Series";
        System.Random rnd = new System.Random();
        ItemSeries.Series id = ItemSeries.Series.Divine;
        Array values = Enum.GetValues(typeof(ItemSeries.Series));
        id = (ItemSeries.Series)values.GetValue(rnd.Next(values.Length));
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
    }

    public override void RemoveEffect(GameObject g)
    {

    }

    public override void AddAditionalStack(GameObject g, Item otherItem)
    {
        System.Random rnd = new System.Random();
        ItemSeries.Series id = ItemSeries.Series.Divine;
        Array values = Enum.GetValues(typeof(ItemSeries.Series));
        id = (ItemSeries.Series)values.GetValue(rnd.Next(values.Length));
        id = (ItemSeries.Series)values.GetValue(rnd.Next(values.Length));
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
        this.series.Add(id);
    }

}
