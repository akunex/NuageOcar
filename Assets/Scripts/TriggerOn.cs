using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOn : MonoBehaviour
{
    public Rigidbody player;

    void OnMouseDown()
    {
        Debug.Log("Touché ------------------------");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>(); ;
        player.velocity = new Vector3(0, 0, 0);
        player.angularVelocity = new Vector3(0, 0, 0);
    }
}
