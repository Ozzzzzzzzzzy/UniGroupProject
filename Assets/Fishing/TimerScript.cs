using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    float DefaultTime = 30f;
    float TimeLeft;

    [SerializeField] TMPro.TextMeshProUGUI TimerText;


    void Start()
    {
        TimeLeft = DefaultTime;
    }

    // Update is called once per frame
    void Update()
    {
        TimeLeft -= Time.deltaTime;
        TimerText.text = ("Time Left: " + Mathf.Ceil(TimeLeft).ToString());

        if (TimeLeft < 0)
        {
            TimeLeft = 0;
            SceneManager.LoadScene("SampleScene");
        }
    }
}
