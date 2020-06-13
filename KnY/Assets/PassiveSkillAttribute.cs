using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute
{
    private string name;


    public virtual void ApplyEffect(GameObject source)
    {

    }

    public virtual void RemoveEffect(GameObject source)
    {

    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }
}
