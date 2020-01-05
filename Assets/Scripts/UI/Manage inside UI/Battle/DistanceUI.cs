using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceUI : MonoBehaviour
{
    public Canvas myCanvas;
    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos+ new Vector2(15,15));
    }
}
