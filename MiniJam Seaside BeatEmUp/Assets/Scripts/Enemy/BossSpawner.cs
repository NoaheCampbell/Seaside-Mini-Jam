using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private EnemyManager enemyManager;
    private bool spawning = false;
    private bool spawnEnemy = false;

    public Transform bossSpawn;

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
                StartCoroutine(SpawnBoss());
            }
        }
    }

    IEnumerator SpawnBoss()
    {
        spawning = true;

        // check if enemies are allowed
        if (enemyManager.bossCount < enemyManager.maxBosses)
        {
             spawnEnemy = true;
        }

        yield return new WaitForSeconds(1);

        if (spawnEnemy)
        {
            Debug.Log("Spawn Boss");
            // instantiate random enemy from list of prefabs
            Instantiate(
                enemyManager.bossPrefab,
                bossSpawn.position,
                Quaternion.identity,
                GameObject.Find("EnemyParent").transform
                );

            // add to enemy count
            enemyManager.bossCount += 1;
        }

        spawnEnemy = false;
        spawning = false;
    }
}
