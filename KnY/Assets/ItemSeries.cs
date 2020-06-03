﻿using System.Collections;
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
        Divine = 2,
        Inheritance = 3,
        Fairie = 4,
        Royal = 5
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
                        UpdateSeries(mySeries, Series.Curse, new ItemSeries_Curse());
                        break;
                    case Series.Divine:
                        UpdateSeries(mySeries, Series.Divine, new ItemSeries_Divine());
                        break;
                    case Series.Royal:
                        UpdateSeries(mySeries, Series.Royal, new ItemSeries_Royal());
                        break;
                }
            }
        }
        return mySeries;
    }

    private static void UpdateSeries(Dictionary<ItemSeries.Series, ItemSeries> mySeries, Series series,ItemSeries itemSeries)
    {
        if (mySeries.ContainsKey(series))
        {
            mySeries[series].totalConditionsMet++;
        }
        else
        {
            mySeries.Add(series, itemSeries);
            mySeries[series].totalConditionsMet++;
        }
    }

}
