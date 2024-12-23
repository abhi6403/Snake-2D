using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class GameUIManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI highScoreText;

        public static int score = 0;

        public void IncreaseScore(int increment)
        {
            score += increment;
            RefreshUI();
        }

        public void DecreaseScore(int decrement)
        {
            score -= decrement; 
            RefreshUI();
        }

        private void RefreshUI()
        {
            scoreText.text = "Score : " + score;
        }

        public void UpdateHighScore()
        {
            highScoreText.text = "Your Score : " + score;
        }

        public void UpdateWinner(int player)
        {
            if(player == 1)
            {
                highScoreText.text = "Player 1 Win";
            } else
            {
                highScoreText.text = "Player 2 Win";
            }
        }
    } 
}

