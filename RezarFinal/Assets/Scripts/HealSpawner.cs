using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpawner : MonoBehaviour
{
    public GameObject prefab;

    public float spawnInterval;

    public Transform spawnPoint;

    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnPrefab());
    }

    IEnumerator SpawnPrefab()
    {
        while (isSpawning)
        {
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
