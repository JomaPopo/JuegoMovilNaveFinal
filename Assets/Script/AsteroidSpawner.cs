using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] float spawnRate = 1f;    
    [SerializeField] float spawnInterval = 3f; 

    void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            float waitTime = Random.Range(spawnRate - spawnInterval, spawnRate + spawnInterval);
            yield return new WaitForSeconds(waitTime);

            Instantiate(
                asteroidPrefab,
                transform.position,
                Quaternion.identity
            );
        }
    }
}