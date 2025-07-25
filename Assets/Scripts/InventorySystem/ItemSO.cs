using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();

    public enum StatToChange
    {
        none,
        mothFood1,
        mothFood2,
        mothFood3,
        mothFood4,
        mothFood5,
        mothFood6,
        mothFood7,
        mothFood8,
        testExample
    };
}
