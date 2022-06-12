using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Quaternion targetRotation;
    private Vector3 targetPosition;
    public bool playerIsHit;
    private string objectTag;
    public float distance;
    private EnemyAttacks enemyAttacks;
    private float timer;
    private EnemyMaster enemy;
    private float rangedCooldown;
    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyAttacks = GetComponent<EnemyAttacks>();
        timer = 0;
        enemy = GetComponent<EnemyMaster>();
        rangedCooldown = 1f;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sends out raycasts to determine if the player is near the enemy
        for (int i = 0; i < 150; i++)
        {            

            RaycastHit hit;
            Vector3 rayDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.3f, 0.3f), Random.Range(-1f, 1f));
            bool hitSomething = Physics.Raycast(transform.position, rayDirection, out hit, 100f);

            if (hitSomething)
                objectTag = hit.transform.gameObject.tag;

            if (hitSomething)
            {
                if (objectTag == "Player")
                {
                     // If the raycast hits the player, rotate towards the ray's rotation
                    targetRotation = Quaternion.LookRotation(hit.point - transform.position);

                    // Gets the distance to the player and its position
                    distance = Vector3.Distance(transform.position, hit.point);
                    targetPosition = hit.collider.gameObject.transform.position;

                    // Turns playerIsHit to true for the rest of the loop
                    playerIsHit = true;
                }
            }
            else
            {
                if (i == 0)
                {
                    // If the raycasts didn't hit the player, keep the enemy still
                    targetRotation = transform.rotation;
                    targetPosition = transform.position;
                    playerIsHit = false;
                }
            }
        }

        // If the enemy is more than 40 units away from the player, kill the enemy
        if (distance > 40f)
        {
            enemyHealth.Death();
        }
        
        // If playerIsHit is true, go through multiple checks to see how the enemy should move
        if (playerIsHit)
        {
            // Move towards the player's position and rotate towards them
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, enemy.moveSpeed * Time.deltaTime);
            targetRotation = new Quaternion(0, targetRotation.y, 0, targetRotation.w);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, enemy.rotationSpeed);

            // Shoot the player whenever the cooldown is 0
            if (rangedCooldown <= 0)
            {
                enemyAttacks.RangedAnimation();
                rangedCooldown = 3f;
            }

            // If the enemy is touching the player, deal damage to the player
            if (distance <= 2f)
            {
                PlayerHealth player = GameObject.FindWithTag("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
                player.TakeDamage(enemy.meleeDmg);
            }
        }

        // Adds to all the timers
        timer += Time.deltaTime;
        rangedCooldown -= Time.deltaTime;

    }
}

       
