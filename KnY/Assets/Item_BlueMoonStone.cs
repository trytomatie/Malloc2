using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BlueMoonStone : Item
{

    private int healthRegen = 10;
    private float durration = 3;
    private int healthRegenPerStack = 5;
    private float durrationPerStack = 2;
    public OnDamageEffect_TriggerRegen myEffectReference;

    public Item_BlueMoonStone()
    {
        this.itemId = 6;
        this.itemName = "Blue Moon Stone";
        this.attribute = "Earth";
        this.description = "Regenerate Health when struck.";
        this.detailedDescription = "Regenerate " + healthRegen + "+" + healthRegenPerStack + " per stack Hp when struck";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        myEffectReference = new OnDamageEffect_TriggerRegen(healthRegen + healthRegenPerStack * stacks, durration + durrationPerStack * stacks);
        g.GetComponent<Statusmanager>().onDamageEffects.Add(myEffectReference);
    }

    public override void RemoveEffect(GameObject g)
    {
        myEffectReference.RemoveEffect();
    }

    public override void AddAditionalStack(GameObject g, Item otherItem)
    {
        base.AddAditionalStack(g, otherItem);
    }

}
