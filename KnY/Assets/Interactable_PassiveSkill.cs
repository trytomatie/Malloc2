using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PassiveSkill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>()._customInteractableMethod = Interact;
        GetComponent<Interactable>()._customAlternateInteractableMethod = AlternateInteract;
    }


    private void Interact(GameObject g)
    {
        PassiveSkill p = PassiveSkill.GenerateRandomPassive();
        UI_PassiveSkillExchangeManager.OpenSkillExchangeWindow(p);
        Destroy(gameObject);
    }

    private void AlternateInteract(GameObject g)
    {
    }
}
