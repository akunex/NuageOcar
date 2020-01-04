using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject enemy;
    PlayerStat enemyStats;

    private void Start()
    {
        enemyStats = null;
        enemy = null;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Dwarf MasterM")
        {
            enemy = GameObject.Find(col.gameObject.name);
            enemyStats = enemy.GetComponent<PlayerStat>();
            enemyStats.TakeDamage(20);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject, 5);
        }
            
    }


}
