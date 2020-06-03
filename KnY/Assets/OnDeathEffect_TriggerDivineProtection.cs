using UnityEngine;

public class OnDeathEffect_TriggerDivineProtection : OnDeathEffect
{
    public OnDeathEffect_TriggerDivineProtection()
    {
        effectName = "Trigger Divine Protection";
        description = "Casts Divine Protection on Self on death";
        image = null;
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_DivineProtection(50));
        ItemSeries_Divine.extraCondtionMet = false;
        UI_InfoTitleManager.Show("<Color=red>Conditions Lost:</color> " + new ItemSeries_Divine().seriesName, "Use your second chance well!", 3);
    }

    public override void OnAdditionalApplication(GameObject g, OnDeathEffect s)
    {
    }

}