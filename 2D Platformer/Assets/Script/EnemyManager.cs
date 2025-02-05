using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public CameraFollow cameraFollow;
    public GameObject invisibleWalls;

    private int enemyCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void EnemySpawned()
    {
        enemyCount++;
        Debug.Log("Enemy Spawned. Total enemies: " + enemyCount);
    }

    public void EnemyKilled()
    {
        enemyCount--;
        Debug.Log("Enemy Killed. Remaining enemies: " + enemyCount);

        if (enemyCount <= 0)
        {
            //กล้องกลับที่
            cameraFollow.UnlockCamera();
            if (invisibleWalls != null)
                invisibleWalls.SetActive(false);
        }
    }
}