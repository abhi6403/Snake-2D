using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject gridbox;
    public int rows;
    public int cols;
    private float xcor = 1.0f;
    private float ycor = 0.0f;

    public void Start()
    {

        for(int i = 1; i<=rows; i++)
        {
            for (int j = 1; j<=cols; j++)
            {
                Vector2 pos1 = GetGridPosition(xcor, ycor);
                GameObject box = Instantiate(gridbox, (pos1), Quaternion.identity);
                Debug.Log("printing Square");
                
                ycor += 1;
            }
            xcor += 1;
            ycor = 0.0f;
        }
    }

    private Vector2 GetGridPosition(float xpos, float ypos)
    {
        float xPos = xpos;
        float yPos = ypos;

        return new Vector2(xPos, yPos);
    }
}
