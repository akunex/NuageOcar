using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public int CurrentPA { get; set; }
    public int MaxPA { get; set; }

    public Slider healthbar;
    public Text displayHealth;
    public Text displayPA;

    void Start()
    {
        //PV
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;
        displayHealth.text = CurrentHealth.ToString();
        healthbar.value = CalculateHealth();

        //PA
        MaxPA = 10;
        CurrentPA = MaxPA;
        displayPA.text = CurrentPA.ToString();
    }


    void Update()
    {
        //PV
        if (Input.GetKeyDown(KeyCode.X))
        {
            DealDamages(10);
        }
        displayHealth.text = CurrentHealth.ToString();

        //PA
        if (Input.GetKeyDown(KeyCode.C))
        {
            LosePA(1);
        }
        displayPA.text = CurrentPA.ToString();
    }

    //Perdre des PV
    void DealDamages(float damageValue)
    {
        CurrentHealth -= damageValue;
        healthbar.value = CalculateHealth();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    //Perdre des PA
    void LosePA(int LosePAValue)
    {
        CurrentPA -= LosePAValue;
        if (CurrentPA <= 0)
        {
            CurrentPA = 0;
        }
    }

    //Calcul vie
    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    // Mourir
    void Die()
    {
        CurrentHealth = 0;
        Debug.Log("t mor");
    }
}
