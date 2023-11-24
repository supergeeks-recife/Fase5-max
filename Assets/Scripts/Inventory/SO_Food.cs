using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Item/Food")]
public class SO_Food : SO_Item
{
    public float restorePoints;
    public float durability;

    private void Awake()
    {
        type = ItemType.food;
    }
}
