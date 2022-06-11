using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private EnemyMaster bossMaster;
    private GameObject boss;
    private BossAttacks bossAttacks;
    private BossMovement bossMovement;
    public bool recentlyJumped;
    private float timer;
    private Vector3 bossPosition;
    private float timeSinceLastJump;
    public bool recentlyDashed;
    private float timeSinceLastDash;
    public bool isDashing;

    // Start is called before the first frame update
    void Start()
    {
        bossMaster = gameObject.GetComponent<EnemyMaster>();

        // Changes enemymaster fields to match boss
        bossMaster.EnemyType(gameObject.tag);

        bossMovement = gameObject.GetComponent<BossMovement>();
        bossAttacks = gameObject.GetComponent<BossAttacks>();

        boss = gameObject;
        recentlyJumped = false;
        timer = 0;
        timeSinceLastJump = 0;
        recentlyDashed = false;
        timeSinceLastDash = 0;
        isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (recentlyJumped)
        {
            timeSinceLastJump += Time.deltaTime;

            if (timeSinceLastJump > 10)
            {
            recentlyJumped = false;
            timeSinceLastJump = 0;
            }
        }

        if (recentlyDashed)
        {
            timeSinceLastDash += Time.deltaTime;

            if (timeSinceLastDash > 10)
            {
                recentlyDashed = false;
                timeSinceLastDash = 0;
            }
        }

    }

    public void Jump()
    {
        if (!recentlyJumped && bossMaster.isGrounded && (!bossAttacks.isAttacking || bossAttacks.isUsingUltimate))
        {
            recentlyJumped = true;
            
            // Start the jump animation
            StartCoroutine(JumpAnimation());
        }
    }

    public void Dash()
    {
        if (!recentlyDashed && bossMaster.isGrounded && (!bossAttacks.isAttacking || bossAttacks.isUsingUltimate))
        {
            recentlyJumped = true;

            // Start the jump animation
            StartCoroutine(DashAnimation());
        }
    }

    public void Spin()
    {
        // Start the spin animation
        StartCoroutine(SpinAnimation());
    }

    IEnumerator DashAnimation()
    {
        recentlyDashed = true;
        isDashing = true;

        // Makes the boss dash towards the player
        var timerLeft = 50f;

        // Backs up a little bit before dashing forward
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position - boss.transform.forward, bossMaster.moveSpeed);

        bossMovement.canRotate = false;
        while (timerLeft > 0)
        {
            // Moves the boss forward in a dash, unless they get more than 30 units from the player
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position + boss.transform.forward, bossMaster.dashSpeed);
            yield return new WaitForSeconds(0.01f);

            if (bossMovement.targetDistance > 30)
            {
                bossMovement.canRotate = true;
                yield break;
            }

            if (bossMovement.targetDistance < 5)
            {
                bossAttacks.MeleeAttack();
            }

            timerLeft -= Time.deltaTime;
        }


        bossMovement.canRotate = true;
        isDashing = false;
    }


    IEnumerator JumpAnimation()
    {
        // Makes the boss jump up for a few seconds
        var timerLeft = 30f;

        while (timerLeft > 0)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, bossPosition + new Vector3(0, 10, 0), 1f);
            yield return new WaitForSeconds(0.01f);
            // boss.transform.position = Vector3.MoveTowards(boss.transform.position, bossPosition, 0.5f);
            timerLeft -= 0.5f;
        }

        if (bossAttacks.isUsingUltimate)
        {
            var tempAOE = Instantiate(bossMaster.ultimateAOE, boss.transform);
            bossAttacks.aoe = tempAOE.GetComponent<UltimateAOE>();
        }

        // Shoots a projectile at the player after the boss lands
        bossAttacks.RangedAttack();
    }

    IEnumerator SpinAnimation()
    {
        // Makes the boss spin around for a few seconds
        var timerLeft = 5f;

        while (timerLeft > 0)
        {
            boss.transform.Rotate(0, 0.5f, 0);
            yield return new WaitForSeconds(0.01f);
            timerLeft -= 0.5f;
        }
    }
    
}
