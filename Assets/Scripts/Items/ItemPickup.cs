using UnityEngine;

//Héritage de la classe Interactable pour pouvoir interargir avec l'objet 
public class ItemPickup : Interactable
{
    //Création d'un item
    public Item item;

    //Réécriture de la fonction Interract() définie dans la class Interactable
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    //Rammassez un objet
    void PickUp()
    {
        Debug.Log("Vous rammassez " + item.name);
        //Si j'ai pris l'objet (car mon inventaire peut être full) alors je peut détruire l'objet
        //InventoryItems.instance.Add(item) remplace FindObjectOfType<InventoryItems>().Add(item); qui permet de créer la référence à l'inventaire
        bool wasPickedUp = InventoryItems.instance.Add(item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }

    }
}
