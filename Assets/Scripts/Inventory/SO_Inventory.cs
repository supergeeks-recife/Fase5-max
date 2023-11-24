using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Item/Inventory")]
public class SO_Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public List<ItemSlot> slotList = new List<ItemSlot>();

    public ItemSlot[] equipmentList = new ItemSlot[5];

    public UnityEvent OnChangeEquipment;

    //0 - helm
    //1 - armor
    //2 - weapon
    //3 - shield
    //4 - boot

    public void ChangeEquipment(SO_Item item)
    {
        if(item.type == ItemType.helm)
        {
            equipmentList[0] = new ItemSlot(item, 1, database.GetID[item]);
            OnChangeEquipment.Invoke();
        }
        else if (item.type == ItemType.armor)
        {
            equipmentList[1] = new ItemSlot(item, 1, database.GetID[item]);
            OnChangeEquipment.Invoke();
        }
        else if (item.type == ItemType.weapon)
        {
            equipmentList[2] = new ItemSlot(item, 1, database.GetID[item]);
            OnChangeEquipment.Invoke();
        }
        else if (item.type == ItemType.shields)
        {
            equipmentList[3] = new ItemSlot(item, 1, database.GetID[item]);
            OnChangeEquipment.Invoke();
        }
        else if (item.type == ItemType.boots)
        {
            equipmentList[4] = new ItemSlot(item, 1, database.GetID[item]);
            OnChangeEquipment.Invoke();
        }
    }



    public SO_ItemDatabase database;

    public void AddItem(SO_Item item, int amount)
    {
        bool itemExists = false;

        foreach(ItemSlot slot in slotList)
        {
            if(slot.item == item)
            {
                slot.AddAmount(amount);
                itemExists = true;
                break;
            }
        }

        if(itemExists == false)
        {
            ItemSlot slot = new ItemSlot(item, amount, database.GetID[item]);
            slotList.Add(slot);
        }

        SaveInventory();
    }

    public void OnAfterDeserialize()
    {
        foreach(ItemSlot slot in slotList)
        {
            if (database.GetItem.ContainsKey(slot.id))
            {
                slot.item = database.GetItem[slot.id];
            }
        }

        for(int i = 0; i < equipmentList.Length; i++)
        {
            if (database.GetItem.ContainsKey(equipmentList[i].id))
            {
                equipmentList[i].item = database.GetItem[equipmentList[i].id];
            }
        }
    }

    public void OnBeforeSerialize()
    {
    }

    public void SaveInventory()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = 
            File.Create(Application.persistentDataPath + "/inventory.sav");

        bf.Serialize(file, saveData);
        file.Close();

    }

    public void LoadInventory()
    {
        if(File.Exists(Application.persistentDataPath + "/inventory.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath + "/inventory.sav",
                FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);

            file.Close();
        }
    }

    
    public void SwapItems(ItemSlot item1, ItemSlot item2)
    {
        int i1 = slotList.IndexOf(item1);
        int i2 = slotList.IndexOf(item2);

        slotList[i1] = item2;
        slotList[i2] = item1;
    }

}


[Serializable]
public class ItemSlot
{
    public SO_Item item;
    public int amount;
    public int id; //numero de identificação do item (Dicionário)

    public ItemSlot (SO_Item new_item, int new_amount, int new_id)
    {
        item = new_item;
        amount = new_amount;
        id = new_id;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}

