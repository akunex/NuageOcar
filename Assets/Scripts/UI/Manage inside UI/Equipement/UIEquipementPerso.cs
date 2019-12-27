using UnityEngine;
using UnityEngine.UI;

public class UIEquipementPerso : MonoBehaviour
{
    public Image icon;
    public Button Unequip;

    private void Update()
    {
        if(icon.sprite != null)
        {
            Unequip.interactable = true;
        }
        else
        {
            Unequip.interactable = false;
        }
    }
}
