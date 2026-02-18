using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string HighScoreData = "Fishing.HighScore";

    public int CurrentScore = 0;
    public int HighScore = 0;

    [SerializeField] private TMPro.TextMeshProUGUI ScoreText;
    [SerializeField] private TMPro.TextMeshProUGUI HighScoreText;

    private void Awake()
    {
        HighScore = PlayerPrefs.GetInt(HighScoreData, 0);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Score: " + CurrentScore;
        HighScoreText.text = "High Score: " + HighScore;
    }

    private void Update()
    {
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt(HighScoreData, HighScore);
            PlayerPrefs.Save();
        }

        UpdateScoreText();
    }
}
