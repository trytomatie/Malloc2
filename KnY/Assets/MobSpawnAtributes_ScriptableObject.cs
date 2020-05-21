using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MobSpawnAtributes_ScriptableObject", order = 1)]
public class MobSpawnAtributes_ScriptableObject : ScriptableObject
{
    public string mobName;
    public GameObject instance;
    public double baseCost;
    public double costPerLevel;
}
