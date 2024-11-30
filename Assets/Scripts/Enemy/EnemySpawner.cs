using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnPointPrefab;
    [SerializeField] EnemyWaveSO enemyWaveSO;
    [SerializeField] Transform enemiesParentTransform;
    [SerializeField] Transform collectiblesParentTransform;
    [SerializeField] LevelManager levelManager;

    [Header("Spawn time range:")]
    [SerializeField] float minSpawnTime = 6f;
    [SerializeField] float maxSpawnTime = 8f;

    [Header("Spawn points range")]
    [SerializeField] int minSpawnPoints = 4;
    [SerializeField] int maxSpawnPoints = 8;

    [SerializeField] Collider2D confiner;

    Bounds spawnBounds;

    void Awake()
    {
        spawnBounds = confiner.bounds;
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void OnEnable()
    {
        LevelManager.OnLevelCompleted += ClearAllEnemies;
    }

    void OnDisable()
    {
        LevelManager.OnLevelCompleted -= ClearAllEnemies;
    }

    void ClearAllEnemies()
    {
        foreach (Transform enemy in enemiesParentTransform)
        {
            Destroy(enemy.gameObject);
            // enemy.gameObject.GetComponent<EnemyDeathHandler>().PlayDeathAnimation();
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (PlayerController.Instance && levelManager.EnemiesInWave > 0)
        {
            Vector2[] spawnPoints = GetSpawnPoints();
            foreach (Vector2 spawnPoint in spawnPoints)
            {
                GameObject randomEnemy = GetRandomEnemy();
                if (randomEnemy != null)
                {
                    GameObject instance = Instantiate(spawnPointPrefab, spawnPoint, transform.localRotation, enemiesParentTransform);
                    SpawnPoint instanceSpawnPoint = instance.GetComponent<SpawnPoint>();
                    instanceSpawnPoint?.InitialEnemySetup(collectiblesParentTransform, levelManager);
                    instanceSpawnPoint?.SetEnemyToSpawn(randomEnemy, enemiesParentTransform);
                }
            }
            float spawnRange = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnRange);
        }
    }

    GameObject GetRandomEnemy()
    {
        if (enemyWaveSO.enemyWave.Count <= 0) return null;
        if (enemyWaveSO.enemyWave.Count == 1) return enemyWaveSO.enemyWave[0];
        int randomIndex = Random.Range(0, enemyWaveSO.enemyWave.Count);
        return enemyWaveSO.enemyWave[randomIndex];
    }

    Vector2[] GetSpawnPoints()
    {
        int numberOfPositions = Random.Range(minSpawnPoints, maxSpawnPoints);
        Vector2[] positions = new Vector2[numberOfPositions];
        for (int i = 0; i < numberOfPositions; i++)
        {
            positions[i] = GenerateSpawnPoint();
        }
        return positions;
    }

    Vector2 GenerateSpawnPoint()
    {
        float xPos = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
        float yPos = Random.Range(spawnBounds.min.y, spawnBounds.max.y);
        return new Vector2(xPos, yPos);
    }
}
