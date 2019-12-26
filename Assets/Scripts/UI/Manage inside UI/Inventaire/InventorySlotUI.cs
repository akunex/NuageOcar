using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button removeButton;
    public Button useButton;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        useButton.interactable = true;

    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        useButton.interactable = false;
    }

    public void OneRemoveButton()
    {
        InventoryItems.instance.Remove(item);
        Debug.Log("OneRemoveButton");
    }

    public void UseItem()
    {
 
        if (item != null)
        {
            item.Use();
            Debug.Log("used");
        }
    }
}
