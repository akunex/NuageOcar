using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipement", menuName ="Inventaire/Equipement")]
public class InventoryEquipement : Item
{
    public EquipementSlot equipSlot;


    public int armorModifier;
    public int damageModifier;


    public override void Use()
    {
        base.Use();
        EquipementManager.instance.Equip(this);
        RemoveFromInventory();
    }

}

public enum EquipementSlot { Casque, Plastron, Gants, Pantalon, Chaussures, ArmeG, ArmeD}