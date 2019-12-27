using UnityEngine;
using UnityEngine.UI;

public class UIstats : MonoBehaviour
{

    public Text vitalite;
    public Text pv;
    public Text armure;
    public Text corps;


    void Update()
    {
        vitalite.text = GameObject.Find("Lady Pirate").GetComponent<CharacterStats>().currentHealth.ToString();
        pv.text = GameObject.Find("Lady Pirate").GetComponent<CharacterStats>().maxHealth.ToString();
        armure.text = GameObject.Find("Lady Pirate").GetComponent<CharacterStats>().armor.GetValue().ToString();
        corps.text = GameObject.Find("Lady Pirate").GetComponent<CharacterStats>().damage.GetValue().ToString();


    }
}
