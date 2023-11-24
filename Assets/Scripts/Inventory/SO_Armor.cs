using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/Armor")]
public class SO_Armor : SO_Item
{
    public float defenseBonus;
    public float speedBonus;
    public float durability;


    private void Awake()
    {
        type = ItemType.armor;
    }
}
