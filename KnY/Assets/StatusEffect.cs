using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : Behaviour {

    public string statusName;
    public string description;
    public Sprite image;
    public float duration;
    public bool effectApplied = false;
    public bool urgentEffect = false;



    public virtual void ApplyEffect(GameObject g)
    {

    }

    public virtual void RemoveEffect(GameObject g)
    {
        
    }

    public virtual void OnAdditionalApplication(GameObject g,StatusEffect s)
    {

    }

}
