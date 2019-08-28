using UnityEngine;

// Permet de créer un gameObject item depuis le menu
[CreateAssetMenu(fileName = "Nouveau Item", menuName = "Inventaire/Item")]
public class Item : ScriptableObject
{
    //Créer les caractéristiques de l'item
    new public string name = "Nouveau Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        //Use the item
        Debug.Log("Using" + name);
    }
}
