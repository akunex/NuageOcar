using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    //Permet de créer une référence à l'inventaire dans les autres class
    #region Singleton
    public static InventoryItems instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;
    }
    #endregion

    /*
     Fonction delegate qui permet de savoir quand on update l'inventaire (ajout et suppression d'items) pour pouvoir gérer ensuite l'UI
     La fonction delegate permet de la subdiviser ensuite en méthodes qui seront appelés à chaque fois qu'on trigger un event (onItemChangedCallback.Invoke();)
     
    */
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    //Nombre de slot dans l'inventaire
    public int space = 20;

    //Création de la liste d'item qui servira dans l'inventaire
    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        //Si ce n'est pas un item par défaut (un truc useless)
        if (!item.isDefaultItem)
        {
            //Je compte le nombre de slot dans l'inventaire
            if (items.Count >= space)
            {
                Debug.Log("Pas de place");
                return false;
            }
            //Je l'ajoute dans ma liste d'items
            items.Add(item);

            //Condition pour s'assurer qu'il y a des méthodes lié à se callback pour pas avoir d'érreurs
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            
        }
        return true;
    }
    public void Remove(Item item)
    {
        //Je le supprime de ma liste d'items
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

}
