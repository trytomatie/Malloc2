using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSeries
{
    public int id;
    public string seriesName;
    public string description;
    public int totalConditionsMet = 0;
    public int conditionsNeeded = 1;
    public bool isActive = false;
    public enum Series
    {
        None = 0,
        Curse = 1,
        Inheritance = 2
    }

    public virtual void ApplyEffect(GameObject g)
    {

    }

    public virtual void RemoveEffect(GameObject g)
    {

    }

    public static Dictionary<Series,ItemSeries> CheckSeries(List<Item> items,Dictionary<ItemSeries.Series, ItemSeries> mySeries)
    {
        foreach(ItemSeries series in mySeries.Values)
        {
            series.totalConditionsMet = 0;
        }
        foreach (Item item in items)
        {
            foreach(Series s in item.series)
            {
                switch(s)
                {
                    case Series.Curse:
                        if(mySeries.ContainsKey(Series.Curse))
                        {
                            mySeries[Series.Curse].totalConditionsMet++;
                        }
                        else
                        {
                            mySeries.Add(Series.Curse, new ItemSeries_Curse());
                            mySeries[Series.Curse].totalConditionsMet++;
                        }
                        break;
                }
            }
        }
        return mySeries;
    }

}
