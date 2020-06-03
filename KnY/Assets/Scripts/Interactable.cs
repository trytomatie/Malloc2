using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    private bool disabled;
    public delegate void CustomInteractable(GameObject o);
    public CustomInteractable _customInteractableMethod;
    public delegate void CustomAlternateInteractable(GameObject o);
    public CustomAlternateInteractable _customAlternateInteractableMethod;
    public int _numberOfUses = 2;
    public string _interactablePopupMessage = "Pickup";
    public string _alternateInteractablePopupMessage = "Inspect";
    public Vector3 _popupMessageOffset = new Vector3(0, 0.07f, 0);
    private float delay = 0.5f;
    private float delayTimer = 0;
    private static GameObject popUp;

    public bool successfullInteraction = false;

    public bool Disabled
    {
        get
        {
            return disabled;
        }

        set
        {
            disabled = value;
        }
    }

    public static GameObject PopUp
    {
        get
        {
            if(popUp == null)
            {
                popUp = GameObject.Find("InteractionPopup");
            }
            return popUp;
        }

        set
        {
            popUp = value;
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Call Interactabale Popup from interface
    /// </summary>
    public void CallInteractablePopup()
    {
        PopUp.GetComponent<InteractionPopup>()._target = transform;
        PopUp.GetComponent<InteractionPopup>().offset = _popupMessageOffset;
        if (_numberOfUses > 0)
        {
            PopUp.transform.Find("Text").GetComponent<Text>().text = _interactablePopupMessage;
            if(_customAlternateInteractableMethod != null)
            {
                PopUp.transform.Find("AlternatePopupText").gameObject.SetActive(true);
                PopUp.transform.Find("AlternatePopupText").Find("Text").GetComponent<Text>().text = _alternateInteractablePopupMessage;
            }
            else
            {
                PopUp.transform.Find("AlternatePopupText").gameObject.SetActive(false);
            }
        }
        else
        {
            PopUp.transform.Find("Text").GetComponent<Text>().text = _alternateInteractablePopupMessage;
        }
    }

    /// <summary>
    /// Dismiss Interactable Popup from interface
    /// </summary>
    public static void DismissInteractablePopup()
    {
        PopUp.GetComponent<InteractionPopup>()._target = null;

    }

    /// <summary>
    /// Call Interaction with Object
    /// </summary>
    public void Interact(GameObject g)
    {
        if(delayTimer > 0)
        {
            return;
        }
        if (_numberOfUses <= 0 || Disabled)
        {
            return;
        }
        if (_customInteractableMethod == null)
        {
            print("No Interaction Set!");
        }
        else
        {
            delayTimer = delay;
            _customInteractableMethod.Invoke(g);
            successfullInteraction = true;
        }
    }

    /// <summary>
    /// Call Alternate Interaction with Object
    /// </summary>
    public void AlternateInteract(GameObject g)
    {
        if (delayTimer > 0)
        {
            return;
        }
        if (_numberOfUses <= 0 || Disabled)
        {
            return;
        }
        if (_customAlternateInteractableMethod == null)
        {
            print("No Alternate Interaction Set!");
        }
        else
        {
            delayTimer = delay;
            _customAlternateInteractableMethod.Invoke(g);
            successfullInteraction = true;
        }
    }

    public IEnumerator DisableForSeconds(float seconds)
    {
        Disabled = true;
        yield return new WaitForSecondsRealtime(seconds);
        Disabled = false;
    }
}
