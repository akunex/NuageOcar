using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_opening : MonoBehaviour
{
    public GameObject panel;
    public void OpenPanel()
    {
        Animator animator = panel.GetComponent<Animator>();
        if(animator != null)
        {
            bool isOpen = animator.GetBool("open");
            animator.SetBool("open", !isOpen);
        }
    }
}
