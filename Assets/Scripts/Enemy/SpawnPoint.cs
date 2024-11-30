using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] float spawnWaitTime = 1f;

    GameObject enemyPrefab;
    Transform enemyParentTransform;
    Transform collectiblesParentTransform;
    LevelManager levelManager;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    public void InitialEnemySetup(Transform collectiblesParent, LevelManager levelManager)
    {
        collectiblesParentTransform = collectiblesParent;
        this.levelManager = levelManager;
    }

    public void SetEnemyToSpawn(GameObject enemy, Transform enemyParent)
    {
        enemyPrefab = enemy;
        enemyParentTransform = enemyParent;
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnWaitTime);
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.localRotation, enemyParentTransform);
        enemy.GetComponent<EnemyDeathHandler>()?.Setup(levelManager, collectiblesParentTransform);
        Destroy(gameObject);
    }
}
