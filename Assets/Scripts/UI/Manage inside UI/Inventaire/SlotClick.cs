using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject popup;

    public Canvas parentCanvas;
    Vector3 Offset = Vector3.zero;

    void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        //Demi longueur de la popup
        Offset.Set(50, -15, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {

        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {

        }
        //Affichage popup et move du cadre sur le pointeur
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            popup.SetActive(true);
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, eventData.position, parentCanvas.worldCamera, out movePos);
            popup.transform.position = parentCanvas.transform.TransformPoint(movePos) + Offset;
        }
    }
}

