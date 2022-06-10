using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private EnemyMaster boss;
    private BossController bossController;
    private BossAttacks bossAttacks;
    public float targetDistance;
    public Vector3 targetPosition;
    private Quaternion targetRotation;
    public bool canRotate;

    // Start is called before the first frame update
    void Start()
    {
        boss = gameObject.GetComponent<EnemyMaster>();
        bossController = gameObject.GetComponent<BossController>();
        bossAttacks = gameObject.GetComponent<BossAttacks>();
        canRotate = true;
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

        // If the target rotation is not null, rotate towards the target rotation
        if (targetRotation != null && canRotate)
        { 
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * boss.rotationSpeed);
        }

        // If the player is more than 40 units away, jump towards the player and use a ranged attack
        if (targetDistance > 40)
        {
            bossController.Jump();
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, targetPosition, boss.speedWhileJumping * Time.deltaTime);
        }

        // If the player is less than 10 units away, dash forward and use a melee attack
        else if (targetDistance < 10)
        {
            bossController.Dash();
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, targetPosition, boss.dashSpeed * Time.deltaTime);
        }
    }
}
