using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Caracteristiques : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Canvas parentCanvas;
    Vector3 Offset = Vector3.zero;
    public Canvas equipement;
    public Canvas caracteristiques;
    public Canvas inventaire;
    public Canvas magie;
    public Canvas objectifs;
    public Canvas carte;
    public Canvas option;
    private List<int> canvasToSort;

    void Start()
    {
         ChangeCanvasOrder();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, eventData.position, parentCanvas.worldCamera, out pos);
        Offset = transform.position - parentCanvas.transform.TransformPoint(pos);
        ChangeCanvasOrder();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, eventData.position, parentCanvas.worldCamera, out movePos);
        transform.position = parentCanvas.transform.TransformPoint(movePos) + Offset;
    }

    void ChangeCanvasOrder()
    {
        canvasToSort = new List<int>() { magie.sortingOrder, inventaire.sortingOrder, caracteristiques.sortingOrder, equipement.sortingOrder, objectifs.sortingOrder, carte.sortingOrder, option.sortingOrder };
        for (int el = 0; el < canvasToSort.Count; el++)
        {
            if (canvasToSort[el] > caracteristiques.sortingOrder)
            {
                canvasToSort[el]--;
            }
        }
        caracteristiques.sortingOrder = 7;
        inventaire.sortingOrder = canvasToSort[1];
        magie.sortingOrder = canvasToSort[0];
        equipement.sortingOrder = canvasToSort[3];
        objectifs.sortingOrder = canvasToSort[4];
        carte.sortingOrder = canvasToSort[5];
        option.sortingOrder = canvasToSort[6];
    }

}




