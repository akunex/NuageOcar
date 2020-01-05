using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    public int maxPA = 8;
    public int currentPA { get; private set; }

    public int maxPM = 250;
    public int currentPM { get; private set; }


    private void Awake()
    {
        currentHealth = maxHealth;
        currentPA = maxPA;
        currentPM = maxPM;
    }

    private void Update()
    {

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

    public void Heal(int heal)
    {

         heal = Mathf.Clamp(heal, 0, int.MaxValue);



        currentHealth += heal;
        Debug.Log(transform.name + "gagne " + heal + " pv");

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void LoosePA(int pa)
    {
        currentPA -= pa;
        if(currentPA <= 0)
        {
            NoPA();
        }
    }

    public void LoosePM(int pm)
    {
        currentPM -= pm;
        if (currentPM <= 0)
        {
            NoPM();
        }
    }

    public void WinPA(int pa)
    {
        currentPA += pa;
        if (currentPA >= maxPA)
        {
            currentPA = maxPA;
        }
    }

    public void WinPM(int pm)
    {
        currentPM += pm;
        if (currentPM >= maxPM)
        {
            currentPM = maxPM;
        }
    }

    public virtual void Die()
    {
        //Overwirtten method
        currentHealth = 0;
        Debug.Log(transform.name + " est mort");
    }

    public virtual void NoPA()
    {
        currentPA = 0;
        Debug.Log(transform.name + " n'a plus de PA");
    }

    public virtual void NoPM()
    {
        currentPM = 0;
        Debug.Log(transform.name + " n'a plus de PM");
    }
}
