using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScriptableObject_InteractableSpawnCard", order = 1)]
public class ScriptableObject_InteractableSpawnCard : ScriptableObject
{
    public GameObject instance;
    public float chanceToSpawn;
    public int cost;
}