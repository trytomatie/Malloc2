using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScriptableObject_ChestSpawnTable", order = 1)]
public class ScriptableObject_ChestSpawnTable : ScriptableObject
{
    public float common;
    public float uncommon;
    public float rare;
    public float epic;
    public float legendary;
}