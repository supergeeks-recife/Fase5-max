using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    weapon,
    armor,
    helm,
    boots,
    shields,
    food,
    quest,
    garbage
}



public class SO_Item : ScriptableObject
{
    public GameObject icon;
    public string itemName;
    public ItemType type;
    [TextArea(10,10)]
    public string description;
    public float value;
    public float weight;
}
