using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkSettings : MonoBehaviour
{
    public ChunkType chunkType;
    public bool concluded = false;
    public bool upIsOpen = true;
    public bool downIsOpen = true;
    public bool rightIsOpen = true;
    public bool leftIsOpen = true;
    public bool forceOpenings = false;
    public string mapDebugInfo = "";
    public bool[] adjustedExits = new bool[4];
    public GameObject[] exits;
    private Chunk_TriggerEvent cte;
    public int weight = 100;


    public enum ChunkType
    {
        Standard,
        Boss,
        Treasure,
        Special,
        Trader,
        Spawn
    }
    private void Start()
    {
        cte = GetComponent<Chunk_TriggerEvent>();
    }
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
        adjustedExits = new bool[4];
        exits[0].GetComponent<TilemapRenderer>().enabled = up;
        exits[0].GetComponent<TilemapCollider2D>().enabled = !up;
        adjustedExits[0] = up;

        exits[1].GetComponent<TilemapRenderer>().enabled = down;
        exits[1].GetComponent<TilemapCollider2D>().enabled = !down;
        adjustedExits[1] = down;

        exits[2].GetComponent<TilemapRenderer>().enabled = right;
        exits[2].GetComponent<TilemapCollider2D>().enabled = !right;
        adjustedExits[2] = right;

        exits[3].GetComponent<TilemapRenderer>().enabled = left;
        exits[3].GetComponent<TilemapCollider2D>().enabled = !left;
        adjustedExits[3] = left;
    }

    public bool Concluded
    {
        get
        {
            return concluded;
        }

        set
        {
            cte.DisableBarrier();
            concluded = value;
        }
    }
}
