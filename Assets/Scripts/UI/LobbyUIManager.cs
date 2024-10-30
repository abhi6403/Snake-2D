using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public Button singlePlayButton;
    public Button multiPlayButton;
    public Button quitButton;

    public void Start()
    {
        singlePlayButton.onClick.AddListener(SinglePlayerButtonClick);
        multiPlayButton.onClick.AddListener(MultiPlayerButtonClick);
        quitButton.onClick.AddListener(QuitButtonClick);
    }

    public void SinglePlayerButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void MultiPlayerButtonClick()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
