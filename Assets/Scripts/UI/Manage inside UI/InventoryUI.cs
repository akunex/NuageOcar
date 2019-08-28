using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    InventorySlotUI[] slots;

    //Référence à l'inventaire via le singleton
    InventoryItems inventory;
    
    void Start()
    {
        inventory = InventoryItems.instance;
        //Quand on update l'inventaire ça execute les fonction delegate
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlotUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        Debug.Log("Inventaire mis à jour !");
        for(int i = 0; i< slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
