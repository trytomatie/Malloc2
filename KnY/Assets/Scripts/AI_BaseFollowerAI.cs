using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AI_BaseFollowerAI : AI_BaseAI
{
    public GameObject followTarget;

    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        if (followTarget.GetComponent<Statusmanager>().ContainsStatusEffect(new StatusEffect_ItemSeriesMinionmancer()))
        {
            foreach (Item item in followTarget.GetComponent<Inventory>().ActiveItemList())
            {
                if (!item.series.Contains(ItemSeries.Series.Minionmancer))
                {
                    GetComponent<Inventory>().AddItem(item);
                }
            }
        }
        foreach (Item item in followTarget.GetComponent<Inventory>().ActiveItemList())
        {
            if (item.itemId == 40)
            {
                GetComponent<Inventory>().AddItem(item);
            }
        }
        rb = GetComponent<Rigidbody2D>();
        attackingAnimators.Add(GetComponent<Animator>());
        StartCoroutine(TimerThread());
    }


}
