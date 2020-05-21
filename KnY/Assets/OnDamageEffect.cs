using UnityEngine;

public class OnDamageEffect
{
    public string effectName;
    public string description;
    public Sprite image;
    public float duration;
    public bool removeEffect = false;

    public virtual void ApplyEffect(GameObject g)
    {

    }

    public virtual void RemoveEffect()
    {
        removeEffect = true;
    }

    public virtual void OnAdditionalApplication(GameObject g, OnDamageEffect s)
    {

    }
}