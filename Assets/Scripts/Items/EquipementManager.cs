using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipementManager : MonoBehaviour
{
    #region Singleton
    public static EquipementManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public InventoryEquipement[] currentEquipement;

    public delegate void OnEquipementChanged(InventoryEquipement newItem, InventoryEquipement oldItem);
    public OnEquipementChanged onEquipmentChanged;

    InventoryItems inventory;

    public Image hudCasque;
    public Image hudPlastron;
    public Image hudGants;
    public Image hudPantalon;
    public Image hudChaussures;
    public Image hudArmeG;
    public Image hudArmeD;

    public Sprite rawImage;
    

    void Start()
    {
        rawImage = hudCasque.sprite;
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

        if(slotIndex == 0)
        {
            hudCasque.sprite =  newItem.icon;
          
        }
        else if(slotIndex == 1){
            hudPlastron.sprite = newItem.icon;
        }
        else if (slotIndex == 2)
        {
            hudGants.sprite = newItem.icon;
        }
        else if (slotIndex == 3)
        {
            hudPantalon.sprite = newItem.icon;
        }
        else if (slotIndex == 4)
        {
            hudChaussures.sprite = newItem.icon;
        }
        else if (slotIndex == 5)
        {
            hudArmeG.sprite = newItem.icon;
        }
        else if (slotIndex == 6)
        {
            hudArmeD.sprite = newItem.icon;
        }

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

            if (slotIndex == 0)
            {
                hudCasque.sprite = rawImage;

            }
            else if (slotIndex == 1)
            {
                hudPlastron.sprite = rawImage;
            }
            else if (slotIndex == 2)
            {
                hudGants.sprite = rawImage;
            }
            else if (slotIndex == 3)
            {
                hudPantalon.sprite = rawImage;
            }
            else if (slotIndex == 4)
            {
                hudChaussures.sprite = rawImage;
            }
            else if (slotIndex == 5)
            {
                hudArmeG.sprite = rawImage;
            }
            else if (slotIndex == 6)
            {
                hudArmeD.sprite = rawImage;
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
