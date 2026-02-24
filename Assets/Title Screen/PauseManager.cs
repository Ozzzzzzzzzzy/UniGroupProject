using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PauseMenuPanel;   // Main pause buttons
    public GameObject OptionsMenuPanel; // Options menu within pause menu

    public MonoBehaviour PlayerController;

    private bool IsPaused = false;

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        PauseMenuPanel.SetActive(true);
        OptionsMenuPanel.SetActive(false);

        Time.timeScale = 0f;
        IsPaused = true;

        if (PlayerController != null)
            PlayerController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);

        Time.timeScale = 1f;
        IsPaused = false;

        if (PlayerController != null)
            PlayerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Called by Options button
    public void OpenOptions()
    {
        PauseMenuPanel.SetActive(false);
        OptionsMenuPanel.SetActive(true);
    }

    // Called by Back button inside Options panel
    public void CloseOptions()
    {
        OptionsMenuPanel.SetActive(false);
        PauseMenuPanel.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }

    public void Quit()
    {
        Application.Quit();
    }
}