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
        int timeLevel = UpgradeManager.LoadTimeUpgradeLevel();
        int extraSeconds = UpgradeManager.GetExtraFishingSeconds(timeLevel);

        TimeLeft = DefaultTime + extraSeconds;
    }

    public void AddTime(float seconds)
    {
        TimeLeft += seconds;
    }

    void Update()
    {
        TimeLeft -= Time.deltaTime;
        TimerText.text = "Time Left: " + Mathf.Ceil(TimeLeft).ToString();

        if (TimeLeft < 0f)
        {
            TimeLeft = 0f;

            int currentCurrency = PlayerPrefs.GetInt(CurrencyManager.CurrencyData, 0);
            PlayerPrefs.SetInt(CurrencyManager.CurrencyData, currentCurrency + ScoreManager.CurrentScore);
            PlayerPrefs.Save();

            SceneManager.LoadScene("SampleScene");
        }
    }
}
