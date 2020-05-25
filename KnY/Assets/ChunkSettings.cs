using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSettings : MonoBehaviour
{
    public bool concluded = false;
    public bool upIsOpen = true;
    public bool downIsOpen = true;
    public bool rightIsOpen = true;
    public bool leftIsOpen = true;
    public bool forceOpenings = false;
    // Start is called before the first frame update
    public bool CheckOpeningsAvaiable(bool up,bool down,bool right,bool left)
    {
        if(forceOpenings)
        {
            if(up != upIsOpen || down != downIsOpen || right != rightIsOpen || left != leftIsOpen)
            {
                return false;
            }
        }
        else
        { 
            if(up && !upIsOpen)
            {
                return false;
            }
            if (down && !downIsOpen)
            {
                return false;
            }
            if (right && !rightIsOpen)
            {
                return false;
            }
            if (left && !leftIsOpen)
            {
                return false;
            }
        }
        return true;
    }
}
