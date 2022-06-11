using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Quaternion targetRotation;
    private Vector3 targetPosition;
    [SerializeField] private float speed = 0.05f;
    public bool playerIsHit;
    private string objectTag;
    public float distance;
    private EnemyAttacks enemyAttacks;
    private float timer;
    public float attackDuration;
    public bool recentlyAttackedMelee;
    public bool recentlyAttackedRanged;
    private float timeSinceMeleeAttack;
    private float timeSinceRangedAttack;
    private float attackMeleeCooldown;
    private float attackRangedCooldown;
    private bool shouldMoveBack;
    private EnemyMaster enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyAttacks = GetComponent<EnemyAttacks>();
        timer = 0;
        attackDuration = 1f;
        recentlyAttackedMelee = false;
        recentlyAttackedRanged = false;
        attackMeleeCooldown = 5f;
        attackRangedCooldown = 0.5f;
        shouldMoveBack = false;
        enemy = GetComponent<EnemyMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sends out raycasts to determine if the player is near the enemy
        for (int i = 0; i < 100; i++)
        {            

            RaycastHit hit;
            Vector3 rayDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.3f, 0.3f), Random.Range(-1f, 1f));
            bool hitSomething = Physics.Raycast(transform.position, rayDirection, out hit, 100f);

            if (hitSomething)
                objectTag = hit.transform.gameObject.tag;

            if (hitSomething && objectTag == "Player")
            {
                // If the raycast hits the player, rotate towards the ray's rotation
                targetRotation = Quaternion.LookRotation(hit.point - transform.position);

                // Gets the distance to the player and its position
                distance = Vector3.Distance(transform.position, hit.point);
                targetPosition = hit.collider.gameObject.transform.position;

                // Turns playerIsHit to true for the rest of the loop
                playerIsHit = true;
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
        
        // If playerIsHit is true, go through multiple checks to see how the enemy should move
        if (playerIsHit)
        {
            // If the enemy is under 30 units away from the player, move closer to the player
            if (distance < 30f && distance > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            }

            // If the enemy is less than 0.1 units away from the player and hasn't recently attacked, attack and
            // set recentlyAttacked to true while moving back
            if (distance <= 0.1f && !recentlyAttackedMelee)
            {
                enemyAttacks.MeleeAnimation(targetPosition);

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, -speed * 2);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
                recentlyAttackedMelee = true;
                shouldMoveBack = true;
            }
            // If the enemy has recently attacked, check to see if it should move back
            else if (shouldMoveBack)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, -speed * 2);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

                if (distance > 30f)
                {
                    shouldMoveBack = false;
                }
            }

            // If the enemy is between 30 and 50 units from the player, back away even more (possible switch to ranged mode later)
            if (distance > 30f && distance < 50f && !recentlyAttackedRanged)
            {
                enemyAttacks.RangedAnimation();
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, -speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
                recentlyAttackedRanged = true;
            }

            // If the enemy is more than 60 units from the player, move towards the player
            if (distance > 60f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            }
        }

        // Adds to all the timers
        timer += Time.deltaTime;
        timeSinceMeleeAttack += Time.deltaTime;
        timeSinceRangedAttack += Time.deltaTime;

        // Checks to see if the enemy should attack again
        if (timeSinceMeleeAttack >= attackMeleeCooldown)
        {
            recentlyAttackedMelee = false;
        }

        if (timeSinceRangedAttack >= attackRangedCooldown)
        {
            recentlyAttackedRanged = false;
        }

        // Resets the enemy's y position to its original position
        if (transform.position.y != 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }

    }
}

       
