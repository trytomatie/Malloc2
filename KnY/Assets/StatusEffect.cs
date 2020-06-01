using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffect {

    public string statusName;
    public string description;
    public Sprite image;
    public float duration;
    public bool effectApplied = false;
    public bool hidden = false;
    public Type type;
    public enum Type
    {
        Buff,
        Debuff,
        Series
    }


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
