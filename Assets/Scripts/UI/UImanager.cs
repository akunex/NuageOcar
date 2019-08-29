using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UImanager : MonoBehaviour
{
   public GameObject[] inventoryPopup;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsOverInventoryPopup() || Input.GetMouseButtonDown(1) && !IsOverInventoryPopup())
        {
            inventoryPopup = GameObject.FindGameObjectsWithTag("InventorySlotPopup");
            foreach (GameObject popup in inventoryPopup)
            {
                popup.SetActive(false);
            }
        }      
    }

    public bool IsOverInventoryPopup()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1,
            };

            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; ++i)
                {
                    if (results[i].gameObject.CompareTag("InventorySlotPopup"))
                        return true;
                }
            }
        }
        return false;
    }
}
