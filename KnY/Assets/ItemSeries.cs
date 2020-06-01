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
        ItemSeries_Curse = 1
    }

    public virtual void ApplyEffect(GameObject g)
    {

    }

    public virtual void RemoveEffect(GameObject g)
    {

    }

    public static Dictionary<Series,ItemSeries> CheckSeries(List<Item> items)
    {
        Dictionary<Series, ItemSeries> mySeries = new Dictionary<Series, ItemSeries>();
        foreach(Item item in items)
        {
            foreach(Series s in item.series)
            {
                switch(s)
                {
                    case Series.ItemSeries_Curse:
                        if(mySeries.ContainsKey(Series.ItemSeries_Curse))
                        {
                            mySeries[Series.ItemSeries_Curse].totalConditionsMet++;
                        }
                        else
                        {
                            mySeries.Add(Series.ItemSeries_Curse, new ItemSeries_Curse());
                            mySeries[Series.ItemSeries_Curse].totalConditionsMet++;
                        }
                        break;
                }
            }
        }
        return mySeries;
    }

}
