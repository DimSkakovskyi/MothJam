using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemToSpawn
{
    public GameObject ItemPrefab;
    [Range(0, 100)] public float DropChance;
}
