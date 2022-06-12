using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    private EnemyMaster bossMaster;
    private GameObject boss;
    private BossController bossController;
    private BossMovement bossMovement;
    private bool recentlyAttackedMelee;
    private bool recentlyAttackedRanged;
    private float timer;
    private float timeSinceLastMeleeAttack;
    private float timeSinceLastRangedAttack;
    private float timeSinceLastSpecial;
    private float timeSinceLastUltimate;
    public bool recentlyAttackedSpecial;
    public bool recentlyAttackedUltimate;
    public bool isUsingSpecial;
    public bool isAttacking;
    public bool isUsingUltimate;

    // Start is called before the first frame update
    void Start()
    {
        bossMaster = gameObject.GetComponent<EnemyMaster>();
        boss = gameObject;
        bossController = gameObject.GetComponent<BossController>();
        bossMovement = gameObject.GetComponent<BossMovement>();
        recentlyAttackedMelee = false;
        recentlyAttackedRanged = false;
        recentlyAttackedSpecial = false;
        timer = 0;
        timeSinceLastMeleeAttack = 0;
        timeSinceLastRangedAttack = 0;
        timeSinceLastSpecial = 0;
        isUsingSpecial = false;
        isAttacking = false;
        isUsingUltimate = false;
        timeSinceLastUltimate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        CheckAttackCooldowns();
    }

    public void CheckAttackCooldowns()
    {
        if (recentlyAttackedMelee)
        {
            timeSinceLastMeleeAttack += Time.deltaTime;

            if (timeSinceLastMeleeAttack > 10)
            {
                recentlyAttackedMelee = false;
                timeSinceLastMeleeAttack = 0;
            }
        }

        if (recentlyAttackedRanged)
        {
            timeSinceLastRangedAttack += Time.deltaTime;

            if (timeSinceLastRangedAttack > 10)
            {
                recentlyAttackedRanged = false;
                timeSinceLastRangedAttack = 0;
            }
        }

        if (recentlyAttackedSpecial)
        {
            timeSinceLastSpecial += Time.deltaTime;

            if (timeSinceLastSpecial > 25)
            {
                recentlyAttackedSpecial = false;
                timeSinceLastSpecial = 0;
            }
        }

        if (recentlyAttackedUltimate)
        {
            timeSinceLastUltimate += Time.deltaTime;

            if (timeSinceLastUltimate > 40)
            {
                recentlyAttackedUltimate = false;
                timeSinceLastUltimate = 0;
            }
        }
    }

    public void MeleeAttack()
    {
        if (!recentlyAttackedMelee && !isAttacking)
        {
           StartCoroutine(Melee());
           bossMovement.canMove = false;
        }

        bossMovement.canMove = true;

    }

    public void RangedAttack()
    {
        if (!recentlyAttackedRanged && !isAttacking)
        {
            StartCoroutine(Ranged());
            bossMovement.canMove = false;
        }

        bossMovement.canMove = true;

    }

    public void LaunchSpecialRangedAttack()
    {
        if (!recentlyAttackedSpecial && !isAttacking)
        {
            StartCoroutine(SpecialRanged());
            bossMovement.canMove = false;
        }

        bossMovement.canMove = true;
    }

    IEnumerator Melee()
    {
        recentlyAttackedMelee = true;
        isAttacking = true;

        // Start the melee animation

        if (!bossController.isDashing)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position + boss.transform.forward, 0.05f);
            yield return new WaitForSeconds(2f);
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position - boss.transform.forward, 0.05f);
        }

        isAttacking = false;
    }

    IEnumerator Ranged()
    {
        recentlyAttackedRanged = true;
        isAttacking = true;

        // Start the ranged animation
        Instantiate(bossMaster.projectile);
        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }

    IEnumerator SpecialRanged()
    {
        recentlyAttackedRanged = true;
        isAttacking = true;
        isUsingSpecial = true;

        // Start the special animation
        for (int i = 0; i < 15; i++)
        {
            Instantiate(bossMaster.projectile);
            yield return new WaitForSeconds(0.1f);
        }

        isUsingSpecial = false;
        isAttacking = false;
    }

    IEnumerator UltimateAttackAnimation()
    {
        recentlyAttackedUltimate = true;
        isAttacking = true;
        isUsingUltimate = true;

        // Start the special animation
        bossController.Jump();
        yield return new WaitForSeconds(1f);

        bossController.Dash();
        yield return new WaitForSeconds(1f);


        isUsingUltimate = false;
        isAttacking = false;
    }
       
}
