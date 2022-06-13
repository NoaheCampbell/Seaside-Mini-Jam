using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyManager enemyManager;
    private bool spawning = false;
    private bool spawnBoss = false;
    private bool spawnEnemy = false;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.Find("GameManager").GetComponent<EnemyManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEnemySpawn();
    }

    IEnumerator SpawnEnemy()
    {
        spawning = true;

        // check if enemies are allowed
        if (enemyManager.enemyCount < enemyManager.maxEnemies && gameObject.tag != "BossAreaSpawner")
        {
            // get chance to spawn enemy
            int spawnInt = Random.Range(1, 101);

            // if selected set bool
            if (enemyManager.spawnChance >= spawnInt && enemyManager.spawnChance > 0)
            {
                spawnEnemy = true;
            }
        }

        if (enemyManager.bossAreaEnemies < enemyManager.maxBossAreaEnemies && gameObject.tag == "BossAreaSpawner")
        {
            // get chance to spawn enemy
            int spawnInt = Random.Range(1, 101);

            // if selected set bool
            if (enemyManager.spawnChance >= spawnInt && enemyManager.spawnChance > 0)
            {
                spawnEnemy = true;
            }
        }

        if (enemyManager.bossCount < enemyManager.maxBosses && gameObject.tag == "BossSpawner")
        {
            spawnBoss = true;
        }

        yield return new WaitForSeconds(1);

        if (spawnEnemy)
        {
            // instantiate random enemy from list of prefabs
            Instantiate(
                enemyManager.enemyPrefabs[Random.Range(0, enemyManager.enemyPrefabs.Count)], 
                RandomPointInBounds(gameObject.GetComponent<Collider>().bounds), 
                Quaternion.identity, 
                GameObject.Find("EnemyParent").transform
                );

            // add to enemy count
            enemyManager.enemyCount += 1;
        }

        if (spawnBoss)
        {
            // instantiate random enemy from list of prefabs
            Instantiate(
                enemyManager.bossPrefab, 
                GameObject.Find("BossSpawn").transform.position, 
                Quaternion.identity, 
                GameObject.Find("EnemyParent").transform
                );

            // add to enemy count
            enemyManager.bossCount += 1;
        }

        spawnEnemy = false;
        spawnBoss = false;
        spawning = false;
    }

    // get spawn location
    static Vector3 RandomPointInBounds(Bounds bounds)
    {
        var randomPos = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 1, Random.Range(bounds.min.z, bounds.max.z));

        var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        for (int i = 0; i < 20; i++)
        {
            if (Mathf.Abs(playerPos.x - randomPos.x) < 2 || Mathf.Abs(playerPos.z - randomPos.z) < 2)
            {
                randomPos = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 1, Random.Range(bounds.min.z, bounds.max.z));
            }

            else
            {
                break;
            }
        }

        return randomPos;
    }

    public void CheckForEnemySpawn()
    {
        if (transform.position.x - player.transform.position.x < 10 && player.transform.position.x - transform.position.x < 10)
        {
            if (!spawning)
            {
                StartCoroutine(SpawnEnemy());
            }
        }
    }
}
