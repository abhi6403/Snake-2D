using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.right; 

    public BoxCollider2D spawnArea;

    private void Update()
    {
        SnakeMovement();
    }

    private void FixedUpdate()
    {
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
        }
    }

}
