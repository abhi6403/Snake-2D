using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.right;
    private Vector2 lastHeadPosition;
    private List<Transform> snakeBodyList;

    public Transform snakeBody;
    public BoxCollider2D spawnArea;
    public float bodyPartSpacing = 0.5f;

    private bool hasEaten = false;

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
        }
    }

    private void GrowSnakeBody()
    {
        Transform snakeSegment = Instantiate(this.snakeBody);
        snakeSegment.position = snakeBodyList[snakeBodyList.Count - 1].position;
        snakeBodyList.Add(snakeSegment);
    }

}
