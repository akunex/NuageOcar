using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    bool isFocus = false;
    Transform player;

    //Pour pouvoir créer un empty qui sert de face surlaquel interagir, mettre l'objet si pas besoin
    public Transform interactionTransform;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        //Methode qui peut être réécrite selon les objets avec lesquels ont interagie
        Debug.Log("Interaction avec" + transform.name);

    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
