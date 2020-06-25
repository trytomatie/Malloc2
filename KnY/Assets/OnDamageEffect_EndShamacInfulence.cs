using UnityEngine;

public class OnDamageEffect_EndShamacInfulence : OnDamageEffect
{


    public OnDamageEffect_EndShamacInfulence()
    {
        effectName = "End Shamac Influence";
        description = "Ends the Shamac Influence";
        image = null;
        duration = 0;
    }

    public override void ApplyEffect(GameObject g)
    {
        StatusEffect s = g.GetComponent<Statusmanager>().GetStatusEffectReference(new StatusEffect_ShamacInfulenced(0));
        if(s != null)
        {
            s.duration = 0;
        }
        removeEffect = true;
    }

}