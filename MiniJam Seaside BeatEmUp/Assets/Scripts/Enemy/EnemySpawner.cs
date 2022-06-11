using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyManager enemyManager;
    private bool spawning = false;
    private bool spawnEnemy = false;
    public float spawnChance = 30; // chance to spawn enemy per second

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.Find("GameManager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!spawning)
            {
                StartCoroutine(SpawnEnemy());
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        spawning = true;

        // check if enemies are allowed
        if (enemyManager.enemyCount < enemyManager.maxEnemies)
        {
            // get chance to spawn enemy
            int spawnInt = Random.Range(1, 101);

            // if selected set bool
            if (spawnChance >= spawnInt && spawnChance > 0)
            {
                spawnEnemy = true;
            }
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

        spawnEnemy = false;
        spawning = false;
    }

    // get spawn location
    static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            1,
            //Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}