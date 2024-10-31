using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public Button lobbyButton;
    public Button quitButton;
    


    public void Start()
    {
        lobbyButton.onClick.AddListener(LobbyButtonClick);
        quitButton.onClick.AddListener(QuitButtonClick);
    }

    public void LobbyButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }

  
}
