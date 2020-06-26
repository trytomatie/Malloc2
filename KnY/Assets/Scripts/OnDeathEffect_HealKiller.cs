using UnityEngine;

public class OnDeathEffect_HealKiller : OnDeathEffect
{

    public int healAmount = 0;

    public OnDeathEffect_HealKiller(int healAmount)
    {
        effectName = "Sacrifice Heal";
        description = "Heals my killer.";
        image = null;
        this.healAmount = healAmount;
    }

    public override void ApplyEffect(GameObject g)
    {
        GameObject target = g.GetComponent<Statusmanager>().gameObjectThatDamagedMeLast;
        if(target != null)
        { 
            target.GetComponent<Statusmanager>().Hp += healAmount;
            Director.GetInstance().SpawnDamageText(healAmount.ToString(), target.transform, Color.green, false);
        }
    }

    public override void OnAdditionalApplication(GameObject g, OnDeathEffect s)
    {
        if(((OnDeathEffect_HealKiller)s).healAmount > healAmount)
        {
            healAmount = ((OnDeathEffect_HealKiller)s).healAmount;
        }
    }

}