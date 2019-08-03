using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MusicButton : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource mouseOverMusic;

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOverMusic.Play();
        Debug.Log("test");
    }
}
