﻿using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);



        currentHealth -= damage;
        Debug.Log(transform.name + "subit " + damage + " dégats");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Overwirtten method
        currentHealth = 0;
        Debug.Log(transform.name + " est mort");
    }
}
