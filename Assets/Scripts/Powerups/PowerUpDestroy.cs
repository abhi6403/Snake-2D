using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDestroy : MonoBehaviour
{
    public int powerUpLifeTime = 5;

    private void Update()
    {
        StartCoroutine(DestroyPowerUp(powerUpLifeTime));
    }

    IEnumerator DestroyPowerUp(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
