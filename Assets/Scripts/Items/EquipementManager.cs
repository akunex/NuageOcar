using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementManager : MonoBehaviour
{
    #region Singleton
    public static EquipementManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    InventoryEquipement[] currentEquipement;

    public delegate void OnEquipementChanged(InventoryEquipement newItem, InventoryEquipement oldItem);
    public OnEquipementChanged onEquipmentChanged;

    InventoryItems inventory;

    void Start()
    {
        inventory = InventoryItems.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipementSlot)).Length;
        currentEquipement = new InventoryEquipement[numSlots];
    }

    public void Equip (InventoryEquipement newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        InventoryEquipement oldItem = null;

        if(currentEquipement[slotIndex] != null)
        {
            oldItem = currentEquipement[slotIndex];
            inventory.Add(oldItem);
        }

        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipement[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if(currentEquipement[slotIndex] != null)
        {
            InventoryEquipement oldItem = currentEquipement[slotIndex];
            inventory.Add(oldItem);

            currentEquipement[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }


        }
    }

    void UnequipAll()
    {
        for (int i = 0; i< currentEquipement.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
