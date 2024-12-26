using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MatProject.UI
{
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
            Time.timeScale = 1.0f;
        }

        public void MultiPlayerButtonClick()
        {
            SceneManager.LoadScene(2);
            Time.timeScale = 1.0f;
        }

        public void QuitButtonClick()
        {
            Application.Quit();
        }
    }
}

