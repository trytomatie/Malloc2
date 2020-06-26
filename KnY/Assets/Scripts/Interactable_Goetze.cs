using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Interactable_Goetze : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Interactable>()._customInteractableMethod = Interact;
    }
	
	// Update is called once per frame
	void Update () {
		

	}

    private void Interact(GameObject g)
    {
        GetComponent<Interactable>().Disabled = true;
        GetComponent<Interactable>()._customInteractableMethod = SecondInteract;
        int rndVal = UnityEngine.Random.Range(0, 101);
        StatusEffect effect = null;
        if(rndVal < 5)
        {
            // heavy curse
            effect = new StatusEffect_Curse_HealthDown(36000, 0.05f);
            g.GetComponent<Statusmanager>().ApplyStatusEffect(effect);
            UI_InfoTitleManager.Show("<Color=red>Permanent Curse!!! </color>" + effect.statusName, effect.description, 3);
        }
        else if( rndVal < 20)
        {
            // light Curse
            effect = new StatusEffect_Curse_HealthDown(120, 0.05f);
            g.GetComponent<Statusmanager>().ApplyStatusEffect(effect);

            UI_InfoTitleManager.Show("<Color=red>Light Curse!!! </color>" + effect.statusName, effect.description, 3);
        }
        else if(rndVal < 40)
        {
            // Divine Protection
            effect = new StatusEffect_DivineProtectionOfStrenght(5);
            g.GetComponent<Statusmanager>().ApplyStatusEffect(effect);
            UI_InfoTitleManager.Show("<Color=yellow>Divine Protection!!! </color>" + effect.statusName, effect.description, 3);
        }
        else if (rndVal < 42)
        {
            // Great Divine Protection
            effect = GetDivineProtection(-1);
            g.GetComponent<Statusmanager>().ApplyStatusEffect(effect);
            UI_InfoTitleManager.Show("<Color=yellow>Divine Protection!!! </color>" + effect.statusName, effect.description, 3);
        }
        else if(rndVal < 80)
        {
            // heal target
            g.GetComponent<Statusmanager>().ApplyHeal(g, UnityEngine.Random.Range(120, g.GetComponent<Statusmanager>().maxHp));
            UI_InfoTitleManager.Show("The Götze restored your strength","", 3);
        }
        else if (rndVal < 10)
        {
            // heal target
            UI_InfoTitleManager.Show("The Götze did Nothing", "", 3);
        }
        GetComponent<Light2D>().enabled = false;
    }

    public static StatusEffect GetDivineProtection(int rndVal2)
    {
        if(rndVal2 == -1)
        {
            rndVal2 = UnityEngine.Random.Range(0, 4);
        }
        StatusEffect effect;
        switch (rndVal2)
        {
            case 0:
                effect = new StatusEffect_DivineProtectionOfStrenght(50);
                break;
            case 1:
                effect = new StatusEffect_DivineProtectionOfSoduimKnowledge();
                break;
            case 2:
                effect = new StatusEffect_DivineProtectionOfSwiftness();
                break;
            case 3:
                effect = new StatusEffect_DivineProtectionOfRollingThunder();
                break;
            default:
                effect = new StatusEffect_DivineProtectionOfStrenght(50);
                break;
        }

        return effect;
    }

    private void SecondInteract(GameObject g)
    {
        GetComponent<Interactable>().Disabled = true;
    }
}
