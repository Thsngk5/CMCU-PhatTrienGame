using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerSystem : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject enemyFastPrefab;
    public GameObject enemyStrongPrefab;

    [Header("Settings")]
    public float spawnInterval = 2f;   // Spawn mỗi 2 giây
    public float spawnRange = 5f;      // Phạm vi spawn ngẫu nhiên

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Vị trí ngẫu nhiên
        Vector3 spawnPos = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange),
            0
        );

        // Ngẫu nhiên chọn loại enemy
        int randomChoice = Random.Range(0, 2);
        if (randomChoice == 0)
            Instantiate(enemyFastPrefab, spawnPos, Quaternion.identity);
        else
            Instantiate(enemyStrongPrefab, spawnPos, Quaternion.identity);
    }
}