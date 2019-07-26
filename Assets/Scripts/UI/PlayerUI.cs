using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public Slider healthbar; 

    void Start()
    {
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;

        healthbar.value = CalculateHealth();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DealDamages(10);
        }
    }

    void DealDamages(float damageValue)
    {
        CurrentHealth -= damageValue;
        healthbar.value = CalculateHealth();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    void Die()
    {
        CurrentHealth = 0;
        Debug.Log("t mor");
    }
}
