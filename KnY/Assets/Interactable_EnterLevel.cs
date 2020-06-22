using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_EnterLevel : MonoBehaviour
{
    public int level = 0;
    // Use this for initialization
    void Start()
    {
        GetComponent<Interactable>()._customInteractableMethod = Interact;
    }
    private void Interact(GameObject g)
    {
        SceneManager.LoadScene(level);
    }

    private void SecondInteract(GameObject g)
    {
        GetComponent<Interactable>().Disabled = true;
    }

}
