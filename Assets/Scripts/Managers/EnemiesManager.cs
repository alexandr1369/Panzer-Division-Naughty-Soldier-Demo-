using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    #region Enemy Prefabs

    [SerializeField] private GameObject enemyAPrefab;
    [SerializeField] private GameObject enemyBPrefab;
    [SerializeField] private GameObject enemyBossPrefab;

    #endregion

    [SerializeField] private List<EnemyPortal> portals; // all enemy portals on the scene
    private EnemyPortal lastUsedEnemyPortal; // spawning utility

    [SerializeField] private float spawnCooldown; // enemy spawning delay
    private float currentSpawnCooldown; // current spawning delay
    private int spawnedEnemiesAmount; // current amount of spawned enemies

    private void Start()
    {
        spawnedEnemiesAmount = 0;
        currentSpawnCooldown = 0;
    }
    private void Update()
    {
        // check for spawning
        if(currentSpawnCooldown <= 0)
        {
            SpawnEnemy();
            currentSpawnCooldown = spawnCooldown;
        }
        // continue waiting
        else
        {
            currentSpawnCooldown -= Time.deltaTime;
        }
    }

    // spawn an enemy through spawning portals
    private void SpawnEnemy()
    {
        // get next enemy prefab
        GameObject enemyPrefab = enemyAPrefab;
        switch (GetNextEnemyType())
        {
            case EnemyType.EnemyB: enemyPrefab = enemyBPrefab; break;
            case EnemyType.EnemyBoss: enemyPrefab = enemyBossPrefab; break;
        }

        // spawn enemy on portals in turn
        EnemyPortal portal;
        if (lastUsedEnemyPortal)
            portal = portals.Find(t => t != lastUsedEnemyPortal);
        else
            portal = portals[Random.Range(0, portals.Count)];

        portal.SpawnEnemy(enemyPrefab);
        spawnedEnemiesAmount++;
    }
    // SIMPLE LOGIC
    // get enemy type for current spawn
    // spawn boss enemy every 15 spawned enemies
    private EnemyType GetNextEnemyType()
    {
        EnemyType enemyType;
        if ((spawnedEnemiesAmount + 1) % 16 == 0) enemyType = EnemyType.EnemyBoss;
        else enemyType = Random.Range(0, 2) == 0 ? EnemyType.EnemyA : EnemyType.EnemyB;
        return enemyType;
    }
}
public enum EnemyType
{
    EnemyA,
    EnemyB,
    EnemyBoss
}
