using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class SO_Weapon : SO_Item
{
    public float attackBonus;
    public float magicBonus;
    public float speedBonus;
    public float durability;


    private void Awake()
    {
        type = ItemType.weapon;
    }

}


