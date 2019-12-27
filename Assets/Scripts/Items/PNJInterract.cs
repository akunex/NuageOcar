using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJInterract : Interactable
{
    public GameObject pnj;
    public GameObject quest;

    public override void Interact()
    {
        base.Interact();
        Talk();
    }

    void Talk()
    {
        Debug.Log("Vous parlez à " + pnj.name);
        //Si j'ai pris l'objet (car mon inventaire peut être full) alors je peut détruire l'objet
        //InventoryItems.instance.Add(item) remplace FindObjectOfType<InventoryItems>().Add(item); qui permet de créer la référence à l'inventaire
        quest.SetActive(true);

    }
}
