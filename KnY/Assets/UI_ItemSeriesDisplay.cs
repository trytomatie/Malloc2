using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSeriesDisplay : MonoBehaviour
{

    public ItemSeries series;
    public Image image;
    public Text title;
    public Text count;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = series.image;
        title.text = series.seriesName;
        int maxCount = series.conditionsNeeded;
        int currentCount = series.totalConditionsMet;
        if(maxCount > currentCount)
        {
            image.color = Color.gray;
        }
        else
        {
            image.color = new Color32(219,219,219,255);
        }
        GetComponent<UI_ItemSeriesDisplayOnHover>().itemSeries = series;
        count.text = currentCount + " / " + maxCount;
    }


    public ItemSeries Series
    {
        get
        {
            return series;
        }

        set
        {
            series = value;
        }
    }
}
