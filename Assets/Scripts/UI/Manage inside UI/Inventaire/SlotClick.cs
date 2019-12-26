using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject popup;

    public Canvas parentCanvas;
    Vector3 Offset = Vector3.zero;

    public GameObject[] inventoryPopup;

    void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        //Demi longueur de la popup
        Offset.Set(-50, -15, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ClosePopup();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {

        }
        //Affichage popup et move du cadre sur le pointeur
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("open popup");
            ClosePopup();
            popup.SetActive(true);
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, eventData.position, parentCanvas.worldCamera, out movePos);
            popup.transform.position = parentCanvas.transform.TransformPoint(movePos) + Offset;
        }
    }

    public void ClosePopup()
    {
        inventoryPopup = GameObject.FindGameObjectsWithTag("InventorySlotPopup");
        foreach (GameObject popup in inventoryPopup)
        {
            popup.SetActive(false);
        }
        Debug.Log("close popup");
    }

}

