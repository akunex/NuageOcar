using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUIStatsHUD : MonoBehaviour
{
    public TextMeshProUGUI hudPV_text;
    public Image hudPV_image;
    public GameObject enemy;



    void Update()
    {
        hudPV_text.text = enemy.GetComponent<CharacterStats>().currentHealth.ToString() + "/" + enemy.GetComponent<CharacterStats>().maxHealth.ToString();
        hudPV_image.fillAmount = (((float)enemy.GetComponent<CharacterStats>().currentHealth) / ((float)enemy.GetComponent<CharacterStats>().maxHealth));

    }
}

