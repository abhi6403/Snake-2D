using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class FoodSpawner : MonoBehaviour
{
    public BoxCollider2D foodSpawnArea;
    public int minSpawnTime = 2;
    public int maxSpawnTime = 5;
    public GameObject[] foodList;
    
    
    bool isSpawning = false;

    private void Update()
    {
        if(!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnFood(UnityEngine.Random.Range(minSpawnTime, maxSpawnTime)));
        }
    }

    IEnumerator SpawnFood(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Vector3 pos = GetRandomPosition();

        int numberOfObjects = Random.Range(0, foodList.Length);
        GameObject food = Instantiate(foodList[numberOfObjects],pos,Quaternion.identity);
        isSpawning = false;
    }

    private Vector3 GetRandomPosition()
    {
        Bounds bounds = foodSpawnArea.bounds;

        float xPos = UnityEngine.Random.Range(bounds.min.x,bounds.max.x);
        float yPos = UnityEngine.Random.Range(bounds.min.y,bounds.max.y);

        return new Vector3(Mathf.Round(xPos), Mathf.Round(yPos), 0.0f);
    }
}
