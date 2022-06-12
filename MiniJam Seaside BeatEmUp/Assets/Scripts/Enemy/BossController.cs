using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private EnemyMaster bossMaster;
    private GameObject boss;
    private BossAttacks bossAttacks;
    private BossMovement bossMovement;
    private float timer;
    private Vector3 bossPosition;
    public bool recentlyDashed;
    private float timeSinceLastDash;
    public bool isDashing;
    public bool canDash;

    // Start is called before the first frame update
    void Start()
    {
        bossMaster = gameObject.GetComponent<EnemyMaster>();

        // Changes enemymaster fields to match boss
        bossMaster.EnemyType(gameObject.tag);

        bossMovement = gameObject.GetComponent<BossMovement>();
        bossAttacks = gameObject.GetComponent<BossAttacks>();

        boss = gameObject;
        timer = 0;
        recentlyDashed = false;
        timeSinceLastDash = 0;
        isDashing = false;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        if (recentlyDashed)
        {
            timeSinceLastDash += Time.deltaTime;

            if (timeSinceLastDash > 5)
            {
                recentlyDashed = false;
                timeSinceLastDash = 0;
            }
        }

    }

    public void Dash()
    {
        if (!recentlyDashed && bossMaster.isGrounded && canDash)
        {   
            Debug.Log("Dashing");

            // Start the jump animation
            StartCoroutine(DashAnimation());
        }
    }

    public void SpawnMinions()
    {
        // Spawn minions (not implemented)
    }

    IEnumerator DashAnimation()
    {
        recentlyDashed = true;
        isDashing = true;

        // Makes the boss dash towards the player
        var timerLeft = 40f;

        // Backs up a little bit before dashing forward
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position - boss.transform.forward, bossMaster.moveSpeed);

        bossMovement.canRotate = false;

        while (timerLeft > 0)
        {
            // Moves the boss backwards in a dash while they are less than 40 units from the player
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position - boss.transform.forward, bossMaster.dashSpeed);
            yield return new WaitForSeconds(0.01f);

            if (bossMovement.targetDistance >= 40)
            {
                bossMovement.canRotate = true;
                yield break;
            }

            timerLeft -= 0.5f;
        }

        bossMovement.canRotate = true;
        isDashing = false;
    }
}
