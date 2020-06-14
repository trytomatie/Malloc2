using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_BlankChest : MonoBehaviour {

    public int cost = 3;
    public GameObject spawnItem;
	// Use this for initialization
	void Start () {
        GetComponent<Interactable>()._customInteractableMethod = Interact;
        GetComponent<Interactable>()._interactablePopupMessage += " (" + cost + " mana)";
        GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Interact(GameObject g)
    {
        if(g.GetComponent<Statusmanager>().Mana < cost)
        {
            return;
        }
        g.GetComponent<Statusmanager>().Mana -= cost;
        GetComponent<Animator>().SetBool("isOpen", true);
        GetComponent<Interactable>().Disabled = true;
        GetComponent<Interactable>()._customInteractableMethod = SecondInteract;
        if(spawnItem != null)
        {
            StartCoroutine(SpawnItem());
        }
    }

    private void SecondInteract(GameObject g)
    {
        GetComponent<Interactable>().Disabled = true;
    }

    IEnumerator SpawnItem()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        GameObject item = Instantiate(spawnItem,transform.position,Quaternion.identity);
    }
}
