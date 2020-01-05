using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIstatsHUD : MonoBehaviour
{
    public TextMeshProUGUI hudPV_text;
    public Image hudPV_image;
    public TextMeshProUGUI hudPA_text;
    public TextMeshProUGUI hudPM_text;




    void Update()
    {
        hudPV_text.text = GameObject.Find("Lady Pirate").GetComponent<CharacterStats>().currentHealth.ToString() + "/" + GameObject.Find("Lady Pirate").GetComponent<CharacterStats>().maxHealth.ToString();
        hudPV_image.fillAmount = (((float)GameObject.Find("Lady Pirate").GetComponent<CharacterStats>().currentHealth) / ((float)GameObject.Find("Lady Pirate").GetComponent<CharacterStats>().maxHealth));
        hudPA_text.text = GameObject.Find("Lady Pirate").GetComponent<CharacterStats>().currentPA.ToString();
        hudPM_text.text = GameObject.Find("Lady Pirate").GetComponent<CharacterStats>().currentPM.ToString();
    }
}
