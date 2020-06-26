using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_ActiveSkill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>()._customInteractableMethod = Interact;
        GetComponent<Interactable>()._customAlternateInteractableMethod = AlternateInteract;
    }


    private void Interact(GameObject g)
    {
        Skill p = Skill.GenerateRandomSkill(g);
        UI_ActiveSkillExchangeManager.OpenSkillExchangeWindow(p);
        Destroy(gameObject);
    }

    private void AlternateInteract(GameObject g)
    {
    }
}
