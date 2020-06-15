using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Stairs : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Interactable>()._customInteractableMethod = Interact;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Interact(GameObject g)
    {
        g.GetComponent<Statusmanager>().ApplyHeal(g,g.GetComponent<Statusmanager>().MaxHp);
        GameObject.FindObjectOfType<MapGenerator>().GenerateNewMap();
    }

    private void SecondInteract(GameObject g)
    {
        GetComponent<Interactable>().Disabled = true;
    }

}
