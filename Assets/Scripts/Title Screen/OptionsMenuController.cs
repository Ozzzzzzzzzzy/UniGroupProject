using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuController : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}