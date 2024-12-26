using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MatProject.Game
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        public BoxCollider2D spawnArea;
        [SerializeField]
        public float minSpawnTime;
        [SerializeField]
        public float maxSpawnTime;
        
        private float lastSpawnTime;
    
        public GameObject[] ObjectList;
    
        bool isSpawning = false;

        private void Update()
        {
            if(!isSpawning)
            {
                float waitTime = Random.Range(minSpawnTime, maxSpawnTime);

                if (Time.time > lastSpawnTime + waitTime)
                {
                    isSpawning = true;
                    SpawnObject();
                    lastSpawnTime = Time.time;
                }
                
            }
        }

        private void SpawnObject()
        {
            Vector3 pos = GetRandomPosition();

            int numberOfObjects = Random.Range(0, ObjectList.Length);
            GameObject gameObject = Instantiate(ObjectList[numberOfObjects],pos,Quaternion.identity);
            isSpawning = false;
        }

        private Vector3 GetRandomPosition()
        {
            Bounds bounds = spawnArea.bounds;

            float xPos = UnityEngine.Random.Range(bounds.min.x,bounds.max.x);
            float yPos = UnityEngine.Random.Range(bounds.min.y,bounds.max.y);

            return new Vector3(Mathf.Round(xPos), Mathf.Round(yPos), 0.0f);
        }
    }
}

