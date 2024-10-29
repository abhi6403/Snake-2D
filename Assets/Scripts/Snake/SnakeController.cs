using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.right;
    private Vector2 lastHeadPosition;
    private List<Transform> snakeBodyList;

    public Transform snakeBody;
    public BoxCollider2D spawnArea;
    public float speedUp;
    public int scoreUp;

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

        if(Vector2.Distance(lastHeadPosition, transform.position) > 0.1f)
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && moveDirection != Vector2.down)
        {
            moveDirection = Vector2.up;
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && moveDirection != Vector2.up)
        {
            moveDirection = Vector2.down;
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && moveDirection != Vector2.right)
        {
            moveDirection = Vector2.left;
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && moveDirection != Vector2.left)
        {
            moveDirection = Vector2.right;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void SnakePosition()
    {
        //body movement
        for(int i = snakeBodyList.Count - 1; i > 0; i--)
        {
            snakeBodyList[i].position = snakeBodyList[i - 1].position;
        }

        //head movement
        transform.position += (Vector3)moveDirection;
    }

    protected void WrapSnakeInBounds()
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
        if(other.gameObject.CompareTag("Food") && !hasEaten)
        {
            hasEaten = true;
            Destroy(other.gameObject);
            GrowSnakeBody();
        }else
            if(other.gameObject.CompareTag("Body"))
        {
            ResetSnake();
        }else 
            if(other.gameObject.CompareTag("Shield") )//&& isShieldActive)
        {
            HandleCollisionWithPowerUp(PowerUpType.SHIELD);
            Destroy(other.gameObject);
        }else 
            if(other.gameObject.CompareTag("SpeedUp"))// && isSpeedBosstActive)
        {
            HandleCollisionWithPowerUp(PowerUpType.SPEEDBOOST);
            Destroy(other.gameObject);
        }
        else
            if(other.gameObject.CompareTag("ScoreX") )//&& isScoreMultiplierActive)
        {
            HandleCollisionWithPowerUp(PowerUpType.SCOREMULTIPLIER);
            Destroy(other.gameObject);
        }
    }

    private void GrowSnakeBody()
    {
        Transform snakeSegment = Instantiate(this.snakeBody);
        snakeSegment.position = snakeBodyList[snakeBodyList.Count - 1].position;
        snakeBodyList.Add(snakeSegment);
    }

    private void ResetSnake()
    {
        for(int i = 1; i < snakeBodyList.Count; i++)
        {
            Destroy(snakeBodyList[i].gameObject);
        }

        snakeBodyList.Clear();
        snakeBodyList.Add(this.transform);

        this.transform.position = Vector3.zero;
    }

    private void HandleCollisionWithPowerUp(PowerUpType powerUpType)
    {
        switch(powerUpType)
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
    private IEnumerator ActivateShield()
    {
        isShieldActive = true;
        yield return new WaitForSeconds(5);
        Debug.Log("Shield Deactivated");
        isShieldActive = false;
    }

    private IEnumerator ActivateSpeedBoost()
    {
        isSpeedBosstActive = true;
        yield return new WaitForSeconds(5);
        Debug.Log("SpeedBoost Deactivated");
        isSpeedBosstActive = false;
    }

    private IEnumerator ActivateScoreMultiplier()
    {
        isScoreMultiplierActive = true;     
        yield return new WaitForSeconds(5);
        Debug.Log("ScoreX Deactivated");
        isScoreMultiplierActive = false;
    }

}

public enum PowerUpType
{
    SHIELD,
    SPEEDBOOST,
    SCOREMULTIPLIER,
}
