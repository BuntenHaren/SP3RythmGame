using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    private bool optionsOpen = false;
    private bool videoSettingsOpen = false;
    private bool audioSettingsOpen = false;

    [SerializeField]
    private Health playerHealth;
    [SerializeField]
    private ActiveCharmIcon activeCharmIcon;
    [SerializeField]
    private PassiveCharmIcon passiveCharmIcon;

    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject mainPauseUI;
    [SerializeField]
    private GameObject optionsMenuUI;
    [SerializeField]
    private GameObject audioSettingsUI;
    [SerializeField]
    private GameObject videoSettingsUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(videoSettingsOpen || audioSettingsOpen)
            {
                BackToOptions();
            }
            else if(optionsOpen)
            {
                BackToPauseMenu();
            }
            else if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        mainPauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        activeCharmIcon.OnResume();
        passiveCharmIcon.OnResume();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        mainPauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        activeCharmIcon.OnPause();
        passiveCharmIcon.OnPause();
    }

    public void Options()
    {
        optionsOpen = true;
        mainPauseUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        optionsOpen = false;
        optionsMenuUI.SetActive(false);
        mainPauseUI.SetActive(true);
    }

    public void VideoSettings()
    {
        videoSettingsOpen = true;
        optionsOpen = false;
        optionsMenuUI.SetActive(false);
        videoSettingsUI.SetActive(true);
    }

    public void AudioSettings()
    {
        audioSettingsOpen = true;
        optionsOpen = false;
        optionsMenuUI.SetActive(false);
        audioSettingsUI.SetActive(true);
    }

    public void BackToOptions()
    {
        audioSettingsOpen = false;
        videoSettingsOpen = false;
        optionsOpen = true;
        audioSettingsUI.SetActive(false);
        videoSettingsUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
