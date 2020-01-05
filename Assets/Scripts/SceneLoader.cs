using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene_arena01()
    {
        SceneManager.LoadScene("Arena01");
    }

    public void LoadScene_zonecombat()
    {
        SceneManager.LoadScene("ZoneCombat");
    }

}
