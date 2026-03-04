using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    float DefaultTime = 30f;
    float TimeLeft;

    [SerializeField] TMPro.TextMeshProUGUI TimerText;
    [SerializeField] private ScoreManager ScoreManager;

    void Start()
    {
        TimeLeft = DefaultTime;
    }

    void Update()
    {
        TimeLeft -= Time.deltaTime;
        TimerText.text = ("Time Left: " + Mathf.Ceil(TimeLeft).ToString());

        if (TimeLeft < 0)
        {
            TimeLeft = 0;

            int currentCurrency = PlayerPrefs.GetInt(CurrencyManager.CurrencyData, 0);
            PlayerPrefs.SetInt(CurrencyManager.CurrencyData, currentCurrency + ScoreManager.CurrentScore);
            PlayerPrefs.Save();

            SceneManager.LoadScene("SampleScene");
        }
    }
}
