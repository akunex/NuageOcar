using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_opening : MonoBehaviour
{
    public GameObject panel;
    public GameObject windows;

    public void OpenPanel()
    {
        Animator animator = panel.GetComponent<Animator>();
        if(animator != null)
        {
            bool isOpen = animator.GetBool("open");
            animator.SetBool("open", !isOpen);
        }
    }

    public void ResetPosition()
    {
        windows.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
    }

}
