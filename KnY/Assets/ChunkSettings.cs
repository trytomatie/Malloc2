using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkSettings : MonoBehaviour
{
    public bool concluded = false;
    public bool upIsOpen = true;
    public bool downIsOpen = true;
    public bool rightIsOpen = true;
    public bool leftIsOpen = true;
    public bool forceOpenings = false;
    public string mapDebugInfo = "";

    public GameObject[] exits;
    // Start is called before the first frame update
    public bool CheckOpeningsAvaiable(bool up, bool down, bool right, bool left)
    {
        if (forceOpenings)
        {
            if (up != upIsOpen || down != downIsOpen || right != rightIsOpen || left != leftIsOpen)
            {
                return false;
            }
        }
        else
        {
            if (up && !upIsOpen)
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

    public void AdjustExits(bool up, bool down, bool right, bool left)
    {
        exits[0].GetComponent<TilemapRenderer>().enabled = up;
        exits[0].GetComponent<TilemapCollider2D>().enabled = !up;

        exits[1].GetComponent<TilemapRenderer>().enabled = down;
        exits[1].GetComponent<TilemapCollider2D>().enabled = !down;

        exits[2].GetComponent<TilemapRenderer>().enabled = right;
        exits[2].GetComponent<TilemapCollider2D>().enabled = !right;

        exits[3].GetComponent<TilemapRenderer>().enabled = left;
        exits[3].GetComponent<TilemapCollider2D>().enabled = !left;
    }
}
