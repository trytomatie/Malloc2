
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_RejuvinationFairy : Item {

    public int shieldAmount = 50;
    public float refreshRate = 5;
    public StatusEffect_RejuvinationFairyShield myEffectReference;
    public Item_RejuvinationFairy()
    {
        this.itemId = 29;
        this.attribute = "Water";
        this.itemName = "Rejuvination Fairy";
        this.description = "Gain a shield that periodically refreshes every 5 seconds.";
        this.detailedDescription = "Gain a shield (+ " + shieldAmount + "* g.GetComponent<Statusmanager>().levelize) that refreshes every "+ refreshRate +" seconds";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Faerie);
    }


    public override void ApplyEffect(GameObject g)
    {
        myEffectReference = new StatusEffect_RejuvinationFairyShield(360000, (int)(shieldAmount * g.GetComponent<Statusmanager>().level * effectMutliplier));
        g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectReference);
    }

    public override void RemoveEffect(GameObject g)
    {
        if(myEffectReference != null)
        { 
            myEffectReference.duration = 0;
        }
    }
}
