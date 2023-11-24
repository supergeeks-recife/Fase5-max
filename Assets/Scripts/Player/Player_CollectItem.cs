using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_CollectItem : MonoBehaviour
{
    public SO_Inventory inventory;

    public UnityEvent OnPlayerCollectItem;

    private void OnTriggerEnter(Collider other)
    {
        Item itemColetado = other.GetComponent<Item>();

        if (itemColetado)
        {
            inventory.AddItem(itemColetado.item, 1);
            Destroy(other.gameObject);
            OnPlayerCollectItem.Invoke();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            inventory.LoadInventory();
            OnPlayerCollectItem.Invoke();
        }
    }

    /*
    private void OnApplicationQuit()
    {
        inventory.slotList.Clear();
    }
    */
}
