using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public BoxCollider2D spawnArea;
    public int minSpawnDuration = 5;
    public int maxSpawnDuration = 5;
    public GameObject[] powerUpsList;

    bool isSpawning = false;

    private void Update()
    {
        if(!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnPowerUp(UnityEngine.Random.Range(minSpawnDuration, maxSpawnDuration)));
        }
    }

    IEnumerator SpawnPowerUp(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Vector3 pos = GetRandomPosition();

        int numberofObjects = UnityEngine.Random.Range(0, powerUpsList.Length);
        GameObject powerup = Instantiate(powerUpsList[numberofObjects], pos, Quaternion.identity);
        isSpawning = false;
    }

    private Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;

        float xPos = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float yPos = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(Mathf.Round(xPos), Mathf.Round(yPos), 0.0f);
    }
}
