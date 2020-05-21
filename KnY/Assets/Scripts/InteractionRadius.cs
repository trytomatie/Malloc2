using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handler for all Player-Interactions
/// </summary>
public class InteractionRadius : MonoBehaviour {

    public List<Interactable> interactables = new List<Interactable>();
    public Interactable _target;
    public float _targetDistance = 0.25f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _target = null;
        float targetDistance = _targetDistance;
        foreach (Interactable interactiable in interactables)
        {
            if (interactiable.Disabled || interactiable == null)
            {
                continue;
            }
            float distance = Vector2.Distance(transform.position, interactiable.transform.position);
            if (targetDistance > distance)
            {
                _target = interactiable;
                targetDistance = distance;
            }
        }
        if (_target != null)
        {
            if(_target.Disabled)
            {
                Interactable.DismissInteractablePopup();
                return;
            }
            _target.CallInteractablePopup();
        }
        else
        {
            Interactable.DismissInteractablePopup();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && !interactables.Contains(interactable))
        {
            interactables.Add(interactable);
        }
    }
}
