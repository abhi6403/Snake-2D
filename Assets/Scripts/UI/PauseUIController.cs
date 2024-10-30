using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUIController : MonoBehaviour
{
    public Button lobbyButton;
    public Button pauseButton;
    public Button resumeButton;
    public GameObject pauseUI;


    public void Start()
    {
        lobbyButton.onClick.AddListener(LobbyButtonClick);
        pauseButton.onClick.AddListener(PauseButtonClick);
        resumeButton.onClick.AddListener(ResumeButtonClick);
    }

    public void LobbyButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseButtonClick()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeButtonClick()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
