using System;
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
    public int stacks;
    public enum Type
    {
        Buff,
        Debuff,
        Series,
        Curse,
        Protection
    }


    public virtual void ApplyEffect(GameObject g)
    {

    }

    public virtual void RemoveEffect(GameObject g)
    {
        
    }

    public virtual void RemoveStacks(int reduce)
    {
        stacks -= reduce;
        if(stacks <= 0)
        {
            duration = 0;
        }
    }

    public virtual void OnAdditionalApplication(GameObject g,StatusEffect s)
    {

    }

    public virtual StatusEffect Copy()
    {
        Debug.LogError("Trying to copy a Statuseffect that can't be copied / ");
        return null;
    }

}
