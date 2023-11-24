using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUD_Inventory : MonoBehaviour
{
    public SO_Inventory inventory;
    public GameObject itemPanel;
    MouseItem mouseItem = new MouseItem();

    public Dictionary<GameObject, ItemSlot> GetItemByIcon;


    public GameObject equipmentPanel;
    public GameObject helmIcon;
    public GameObject armorIcon;
    public GameObject weaponIcon;
    public GameObject shieldIcon;
    public GameObject bootIcon;
    public bool isChangingEquipment;




    void Start()
    {
        CreateInventoryIcons();
    }

    public void CreateInventoryIcons()
    {
        GetItemByIcon = new Dictionary<GameObject, ItemSlot>();

        foreach(ItemSlot slot in inventory.slotList)
        {
            GameObject icon = Instantiate(slot.item.icon, itemPanel.transform);
            icon.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString();

            GetItemByIcon.Add(icon, slot);

            AddEvent(icon, EventTriggerType.PointerEnter, delegate { OnPointerEnter(icon); });
            AddEvent(icon, EventTriggerType.PointerExit, delegate { OnPointerExit(icon); });
            AddEvent(icon, EventTriggerType.BeginDrag, delegate { OnDragStart(icon); });
            AddEvent(icon, EventTriggerType.EndDrag, delegate { OnDragEnd(icon); });
            AddEvent(icon, EventTriggerType.Drag, delegate { OnDrag(icon); });
        }

        CreateEquipmentIcons(helmIcon, 0);
        CreateEquipmentIcons(armorIcon, 1);
        CreateEquipmentIcons(weaponIcon, 2);
        CreateEquipmentIcons(shieldIcon, 3);
        CreateEquipmentIcons(bootIcon, 4);
    }

    public void CreateEquipmentIcons(GameObject equipIcon, int index)
    {
        equipIcon.GetComponent<Image>().sprite = inventory.equipmentList[index].item.icon.GetComponent<Image>().sprite;
        GetItemByIcon.Add(equipIcon, inventory.equipmentList[index]);

        AddEvent(equipIcon, EventTriggerType.PointerEnter, delegate { OnPointerEnter(equipIcon); });
        AddEvent(equipIcon, EventTriggerType.PointerExit, delegate { OnPointerExit(equipIcon); });
        AddEvent(equipIcon, EventTriggerType.BeginDrag, delegate { OnDragStart(equipIcon); });
        AddEvent(equipIcon, EventTriggerType.EndDrag, delegate { OnDragEnd(equipIcon); });
        AddEvent(equipIcon, EventTriggerType.Drag, delegate { OnDrag(equipIcon); });
    }

    void OnPointerEnter(GameObject obj)
    {
        if (GetItemByIcon.ContainsKey(obj))
        {
            if(obj.transform.parent.gameObject == equipmentPanel)
            {
                isChangingEquipment = true;
            }
            mouseItem.itemHover = GetItemByIcon[obj];
        }
    }

    void OnPointerExit(GameObject obj)
    {
        if (obj.transform.parent.gameObject == equipmentPanel)
        {
            isChangingEquipment = false;
        }
        mouseItem.itemHover = null;
    }

    void OnDragStart(GameObject obj)
    {
        GameObject mouseIcon = new GameObject();

        RectTransform rt = mouseIcon.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(32, 32);
        mouseIcon.transform.SetParent(transform);

        Image img = mouseIcon.AddComponent<Image>();
        img.sprite = obj.GetComponent<Image>().sprite;
        img.raycastTarget = false;

        mouseItem.obj = mouseIcon; 
        mouseItem.itemClicked = GetItemByIcon[obj]; 
    }

    void OnDragEnd(GameObject obj)
    {
        if (mouseItem.itemHover != null)
        {
            if (isChangingEquipment)
            {
                inventory.ChangeEquipment(mouseItem.itemClicked.item);
            }
            else
            {
                inventory.SwapItems(mouseItem.itemClicked, mouseItem.itemHover);
            }
            
            UpdateInventoryIcons();
        }

        Destroy(mouseItem.obj);
        mouseItem.itemClicked = null;
    }

    void OnDrag(GameObject obj)
    {
        if (mouseItem.obj)
        {
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }


    public void UpdateInventoryIcons()
    {
        foreach(Transform child in itemPanel.transform)
        {
            Destroy(child.gameObject);
        }

        CreateInventoryIcons();
    }



    void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    
}


public class MouseItem
{
    public GameObject obj;
    public ItemSlot itemClicked; 
    public ItemSlot itemHover; 
}