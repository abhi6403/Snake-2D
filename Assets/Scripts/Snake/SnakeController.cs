using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;

    public BoxCollider2D spawnArea;

    private void Update()
    {
        SnakeMovement();
        WrapSnakeInBounds();
    }

    private void FixedUpdate()
    {
       SnakePosition();
    }

    private void SnakeMovement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    private void SnakePosition()
    {
        this.transform.position = new Vector3(
           Mathf.Round(this.transform.position.x) + direction.x,
           Mathf.Round(this.transform.position.y) + direction.y,
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
}
