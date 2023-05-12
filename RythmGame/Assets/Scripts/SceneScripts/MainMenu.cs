using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool optionsOpen = false;
    private bool videoSettingsOpen = false;
    private bool audioSettingsOpen = false;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (videoSettingsOpen || audioSettingsOpen)
            {
                BackToOptions();
            }
            else if (optionsOpen)
            {
                BackToMainMenu();
            }
        }
    }

    public void Options()
    {
        optionsOpen = true;
        mainPauseUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void BackToMainMenu()
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

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
