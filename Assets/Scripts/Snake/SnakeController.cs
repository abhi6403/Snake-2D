using System.Collections;
using System.Collections.Generic;
using Food;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snake
{
    public class SnakeController : MonoBehaviour
    {
        public string snakeID;
        private Vector2 moveDirection = Vector2.right;
        private Vector2 lastHeadPosition;
        private List<Transform> snakeBodyList;

        public UI.GameUIManager gameUIManager;
        public GameObject gameOverObject;
        public GameObject gameUI;
        public GameObject speedPowerUp;
        public GameObject shieldPowerUp;
        public GameObject scorePowerUp;
        public Transform snakeBody;
        public BoxCollider2D spawnArea;
        public float speedUp;

        public KeyCode upKey = KeyCode.UpArrow;
        public KeyCode downKey = KeyCode.DownArrow;
        public KeyCode leftKey = KeyCode.LeftArrow;
        public KeyCode rightKey = KeyCode.RightArrow;

        private bool hasEaten = false;
        private bool isShieldActive = false;
        private bool isSpeedBosstActive = false;
        private bool isScoreMultiplierActive = false;

        public void Start()
        {
            snakeBodyList = new List<Transform>();
            snakeBodyList.Add(this.transform);
            lastHeadPosition = transform.position;
        }

        private void Update()
        {
            SnakeMovement();

            if (Vector2.Distance(lastHeadPosition, transform.position) > 0.1f)
            {
                hasEaten = false;
                lastHeadPosition = transform.position;
            }
        }

        private void FixedUpdate()
        {
            lastHeadPosition = transform.position;
            SnakePosition();
            WrapSnakeInBounds();
        }

        private void SnakeMovement()
        {
            if (snakeID == "SnakeOne" || snakeID == "SnakeTwo")
            {
                if (Input.GetKeyDown(upKey) && moveDirection != Vector2.down)
                {
                    moveDirection = Vector2.up;
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (Input.GetKeyDown(downKey) && moveDirection != Vector2.up)
                {
                    moveDirection = Vector2.down;
                    transform.eulerAngles = new Vector3(0, 0, -90);
                }
                else if (Input.GetKeyDown(leftKey) && moveDirection != Vector2.right)
                {
                    moveDirection = Vector2.left;
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (Input.GetKeyDown(rightKey) && moveDirection != Vector2.left)
                {
                    moveDirection = Vector2.right;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
        }

        private void SnakePosition()
        {
            {
                SnakeBodyMovement();
                SnakeHeadMovement();
            }

        }

        private void SnakeHeadMovement()
        {
            //head movement
            if (isSpeedBosstActive == true)
            {
                transform.position += (Vector3)moveDirection * speedUp;

            }
            else
            {
                transform.position += (Vector3)moveDirection;
            }
        }
        private void SnakeBodyMovement()
        {
            //body movement
            for (int i = snakeBodyList.Count - 1; i > 0; i--)
            {
                snakeBodyList[i].position = snakeBodyList[i - 1].position;
            }
        }
        protected void WrapSnakeInBounds() //method to wrap snake in bounds
        {
            Bounds bounds = spawnArea.bounds;

            Vector3 snakeHeadPosition = this.transform.position;

            if (snakeHeadPosition.x > bounds.max.x)
            {
                snakeHeadPosition.x = bounds.min.x;
            }
            else if (snakeHeadPosition.x < bounds.min.x)
            {
                snakeHeadPosition.x = bounds.max.x;
            }

            if (snakeHeadPosition.y > bounds.max.y)
            {
                snakeHeadPosition.y = bounds.min.y;
            }
            else if (snakeHeadPosition.y < bounds.min.y)
            {
                snakeHeadPosition.y = bounds.max.y;
            }

            this.transform.position = snakeHeadPosition;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //collision with food
            if (other.gameObject.CompareTag("MassGainer") && !hasEaten)
            {
                HandleCollisionWithFood(Food.FoodType.MASSGAINER);
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("MassBurner") && !hasEaten)
            {
                HandleCollisionWithFood(Food.FoodType.MASSBURNER);
                Destroy(other.gameObject);
            }
            else
                //checking for collision with SnakeOneBody
            if (other.gameObject.CompareTag("BodyOne") && isShieldActive == false)
            {
                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
                {
                    showGameOverUI();
                    gameUIManager.UpdateHighScore();
                }
                else
                {
                    showGameOverUI();
                    gameUIManager.UpdateWinner(2);
                }
            }
            else
                //checking for collision with SnakeTwoBody
            if (other.gameObject.CompareTag("BodyTwo") && isShieldActive == false)
            {
                showGameOverUI();
                gameUIManager.UpdateWinner(1);
            }
            else
                //collision with shield
            if (other.gameObject.CompareTag("Shield") && isShieldActive == false)
            {
                HandleCollisionWithPowerUp(PowerUpType.SHIELD);
                Destroy(other.gameObject);
            }
            else
                //collision with speedup
            if (other.gameObject.CompareTag("SpeedUp") && isSpeedBosstActive == false)
            {
                HandleCollisionWithPowerUp(PowerUpType.SPEEDBOOST);
                Destroy(other.gameObject);
            }
            else
                //collision with scoreMultiplier
            if (other.gameObject.CompareTag("ScoreX") && isScoreMultiplierActive == false)
            {
                HandleCollisionWithPowerUp(PowerUpType.SCOREMULTIPLIER);
                Destroy(other.gameObject);
            }
        }

        private void showGameOverUI()
        {
            gameOverObject.SetActive(true);
            gameUI.gameObject.SetActive(false);
            Time.timeScale = 0f;
        }

        private void GrowSnakeBody()
        {
            Transform snakeSegment = Instantiate(this.snakeBody);
            snakeSegment.position = snakeBodyList[snakeBodyList.Count - 1].position;
            snakeBodyList.Add(snakeSegment);
        }

        private void ResetSnake()
        {
            for (int i = 1; i < snakeBodyList.Count; i++)
            {
                Destroy(snakeBodyList[i].gameObject);
            }

            snakeBodyList.Clear();
            snakeBodyList.Add(this.transform);

            this.transform.position = new Vector3(30.0f, 30.0f, 0.0f);
        }

        private void ReduceSnakeBody()
        {
            if (snakeBodyList.Count > 1)
            {
                Transform lastSegment = snakeBodyList[snakeBodyList.Count - 1];
                snakeBodyList.RemoveAt(snakeBodyList.Count - 1);
                Destroy(lastSegment.gameObject);
            }
        }

        private void HandleCollisionWithPowerUp(PowerUpType powerUpType)
        {
            switch (powerUpType)
            {
                case PowerUpType.SHIELD:
                    StartCoroutine(ActivateShield());
                    Debug.Log("Shield Active");
                    break;

                case PowerUpType.SPEEDBOOST:
                    StartCoroutine(ActivateSpeedBoost());
                    Debug.Log("Speed Active");
                    break;

                case PowerUpType.SCOREMULTIPLIER:
                    StartCoroutine(ActivateScoreMultiplier());
                    Debug.Log("ScoreX Active");
                    break;
            }
        }

        private void HandleCollisionWithFood(FoodType foodType)
        {
            switch (foodType)
            {
                case FoodType.MASSGAINER:
                    hasEaten = true;
                    GrowSnakeBody();
                    ScoreMultiplier();
                    break;

                case FoodType.MASSBURNER:
                    hasEaten = true;
                    ReduceSnakeBody();
                    gameUIManager.DecreaseScore(1);
                    break;
            }
        }

        private void ScoreMultiplier()
        {
            if (isScoreMultiplierActive == true)
            {
                gameUIManager.IncreaseScore(2);
            }
            else
            {
                gameUIManager.IncreaseScore(1);
            }
        }

        private IEnumerator ActivateShield()
        {
            isShieldActive = true;
            shieldPowerUp.SetActive(true);
            yield return new WaitForSeconds(5);
            shieldPowerUp.SetActive(false);
            Debug.Log("Shield Deactivated");
            isShieldActive = false;
        }

        private IEnumerator ActivateSpeedBoost()
        {
            isSpeedBosstActive = true;
            speedPowerUp.SetActive(true);
            yield return new WaitForSeconds(5);
            speedPowerUp.SetActive(false);
            Debug.Log("SpeedBoost Deactivated");
            isSpeedBosstActive = false;
        }

        private IEnumerator ActivateScoreMultiplier()
        {
            isScoreMultiplierActive = true;
            scorePowerUp.SetActive(true);
            yield return new WaitForSeconds(5);
            scorePowerUp.SetActive(false);
            Debug.Log("ScoreX Deactivated");
            isScoreMultiplierActive = false;
        }

    }

}
