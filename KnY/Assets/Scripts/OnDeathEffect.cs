using UnityEngine;

public class OnDeathEffect
{
    public string effectName;
    public string description;
    public Sprite image;
    public bool removeEffect = false;

    public virtual void ApplyEffect(GameObject g)
    {

    }

    public virtual void RemoveEffect()
    {
        removeEffect = true;
    }

    public virtual void OnAdditionalApplication(GameObject g, OnDeathEffect s)
    {

    }
}