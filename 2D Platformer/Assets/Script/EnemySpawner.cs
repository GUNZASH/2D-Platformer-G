using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public GameObject invisibleWalls;

    private CameraFollow cameraFollow;
    private bool hasTriggered = false;

    void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        if (invisibleWalls != null)
            invisibleWalls.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            Debug.Log("Player entered the trigger. Spawning enemies...");
            gameObject.SetActive(false);

            SpawnEnemies();
            cameraFollow.LockCamera(); // ล็อคกล้อง

            if (invisibleWalls != null)
                invisibleWalls.SetActive(true);
        }
    }

    void SpawnEnemies()
    {
        int enemiesToSpawn = spawnPoints.Length;
        Debug.Log("Spawning " + enemiesToSpawn + " enemies");

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            EnemyManager.Instance.EnemySpawned();
        }
    }
}