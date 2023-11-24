using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Database")]
public class SO_ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public SO_Item[] items;

    public Dictionary<SO_Item, int> GetID = new Dictionary<SO_Item, int>();
    public Dictionary<int, SO_Item> GetItem = new Dictionary<int, SO_Item>();


    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<SO_Item, int>();
        GetItem = new Dictionary<int, SO_Item>();

        for(int i = 0; i < items.Length; i++)
        {
            GetID.Add(items[i], i);  
            GetItem.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
