using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Le radius correspond à la "hitbox" de l'objet
    public float radius = 3f;
    bool isFocus = false;
    Transform player;

    //Pour pouvoir créer un empty qui sert de face sur laquel interagir (coté sur lequel ouvrir un coffre par exemple), si pas besoin: pas créer d'empty et mettre directement l'objet en lui même
    public Transform interactionTransform;

    bool hasInteracted = false;

    //Methode qui peut être réécrite selon les "objets" avec lesquels ont interagie (Par exemple, pour item, on interagie en rammassant l'item)
    public virtual void Interact()
    {
        Debug.Log("Interaction avec" + transform.name);
    }

    private void Update()
    {
        //Si on selectionne l'objet et qu'on a pas encore interagi avec
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            //Si on est à la bonne distance avec la hitbox de l'objet (le radius)
            if (distance <= radius)
            {
                Interact();
                //Permet de ne pouvoir avoir qu'une interaction "par clique" (ou pas selon comment on réécris Interact() selon les différents interaction possible) avec l'objet
                hasInteracted = true;
            }
        }
    }

    //Lors d'un focus (Permet l'interaction dans le Update())
    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    //Lors d'un defocus (Evite l'interaction dans le Update())
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    //Permet de voir la hitbox
    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
