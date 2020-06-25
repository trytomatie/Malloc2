using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ShamacInfulenced : StatusEffect {


    GameObject indicator;
    public StatusEffect_ShamacInfulenced(float durration)
    {
        this.statusName = "Shamac Influenced";
        this.description = "This unit has lost all their senses. Breaks on damage.";
        this.image = ItemIcons.GetIconFromInstance(0);
        this.duration = durration;
        this.type = Type.Debuff;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            indicator = GameObject.Instantiate(Director.GetInstance().statusEffectIndicator, g.transform.position + new Vector3(0,g.GetComponent<Statusmanager>().uiHeigthOffset,0), Quaternion.identity, g.transform);
            indicator.GetComponent<Animator>().SetInteger("AnimationState", 1);
            GameObject.Destroy(indicator, duration);
            g.GetComponent<Statusmanager>().onDamageEffects.Add(new OnDamageEffect_EndShamacInfulence());
            if (g.GetComponent<PlayerController>() != null)
            {
                Director.GetInstance().SetFadeMaterial(0, 1, GameObject.Find("DeathBackground").GetComponent<SpriteRenderer>().material, 2);
            }
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            if (g.GetComponent<PlayerController>() != null)
            {
                Director.GetInstance().SetFadeMaterial(1, 0, GameObject.Find("DeathBackground").GetComponent<SpriteRenderer>().material, 2);
            }
            if(indicator != null)
            { 
                GameObject.Destroy(indicator);
            }
            effectApplied = false;
        }
    }
}
