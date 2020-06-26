using UnityEngine;

public class OnDamageEffect_TriggerRegen :OnDamageEffect
{

    private int strength = 0;
    private float regenDuration = 0;

    public OnDamageEffect_TriggerRegen(int strength, float regenDuration)
    {
        effectName = "Trigger Regen";
        description = "Triggers a regeneration effect on hit";
        image = null;
        duration = 0;
        this.strength = strength;
        this.regenDuration = regenDuration;
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_Regen(regenDuration, strength));
    }

}