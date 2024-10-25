using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.right;
    private List<Transform> snakeMovePositionList;

    public Transform snakeBody;
    public BoxCollider2D spawnArea;

    public void Start()
    {
        snakeMovePositionList = new List<Transform>();
        snakeMovePositionList.Add(this.transform);
    }
    private void Update()
    {
        SnakeMovement();
    }

    private void FixedUpdate()
    {
       SnakePosition();
       WrapSnakeInBounds();

        for (int i = snakeMovePositionList.Count - 1; i > 0; i--)
        {
            snakeMovePositionList[i].position = snakeMovePositionList[i - 1].position;
        }

        this.transform.position = new Vector3(
        Mathf.Round(this.transform.position.x) + moveDirection.x,
        Mathf.Round(this.transform.position.y) + moveDirection.y,
        0.0f);
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
        this.transform.position = new Vector3(
           Mathf.Round(this.transform.position.x) + moveDirection.x,
           Mathf.Round(this.transform.position.y) + moveDirection.y,
           0.0f
           );
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
        if(other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            GrowSnakeSize(1);
        }
    }

    private void GrowSnakeSize(int length)
    {
        for(int i=0; i<length; i++)
        {
            Transform newSnakeBody = Instantiate(this.snakeBody, new Vector3(-100f, -100f, 0), new Quaternion(0f, 0f, 0f, 0f));
            snakeMovePositionList.Add(newSnakeBody);
        }
    }
}
