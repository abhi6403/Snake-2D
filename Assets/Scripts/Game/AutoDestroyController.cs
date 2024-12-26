using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatProject.Game
{
    public class AutoDestroyController : MonoBehaviour
    {
        public int lifeTime = 5;

        private void Update()
        {
            StartCoroutine(DestroyObject(lifeTime));
        }

        IEnumerator DestroyObject(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}

