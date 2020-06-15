using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_ButtonIsPressed : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onPressedEvent;
    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {
        if (!ispressed)
            return;
        onPressedEvent.Invoke();
    }
    bool ispressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        ispressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ispressed = false;
    }
}
