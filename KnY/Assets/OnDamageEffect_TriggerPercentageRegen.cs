using UnityEngine;

public class OnDamageEffect_TriggerPercentageRegen : OnDamageEffect
{

    private float percentage = 0;
    private float regenDuration = 0;
    private Statusmanager statusRef;

    public OnDamageEffect_TriggerPercentageRegen(float percentage, float regenDuration,Statusmanager statusRef)
    {
        effectName = "Trigger Regen";
        description = "Triggers a regeneration effect on hit";
        image = null;
        duration = 0;
        this.percentage = percentage;
        this.regenDuration = regenDuration;
        this.statusRef = statusRef;
    }

    public override void ApplyEffect(GameObject g)
    {
        int healAmount = (int)(statusRef.maxHp * percentage / regenDuration);
        g.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_Regen(regenDuration, healAmount));
    }

}