using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private EnemyMaster boss;
    private BossController bossController;
    private BossAttacks bossAttacks;
    public float targetDistance;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool recentlyMeleeAttacked;
    private bool recentlyRangedAttacked;


    // Start is called before the first frame update
    void Start()
    {
        boss = gameObject.GetComponent<EnemyMaster>();
        bossController = gameObject.GetComponent<BossController>();
        bossAttacks = gameObject.GetComponent<BossAttacks>();
        recentlyMeleeAttacked = false;
        recentlyRangedAttacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Sends out raycasts to see where the player is
        for (int i = 0; i < 150; i++)
        {
            RaycastHit hit;
            Vector3 rayDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.3f, 0.3f), Random.Range(-1f, 1f));
            bool hitSomething = Physics.Raycast(transform.position, rayDirection, out hit, 100f);

            if (hitSomething)
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    // If the raycast hits the player, rotate towards the ray's rotation
                    targetRotation = Quaternion.LookRotation(hit.point - transform.position);

                    // Gets the distance to the player and its position
                    targetDistance = Vector3.Distance(transform.position, hit.point);
                    targetPosition = hit.collider.gameObject.transform.position;
                }
            }
        }

        // If the player is more than 50 units away, jump towards the player and use a ranged attack
        if (targetDistance > 50)
        {
            bossController.Jump();
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, targetPosition, boss.speedWhileJumping * Time.deltaTime);
            bossAttacks.RangedAttack();
        }
    }
}
