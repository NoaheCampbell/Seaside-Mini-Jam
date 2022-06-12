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
    private float timeSinceLastMeleeAttack;
    private float timeSinceLastRangedAttack;
    private float timeSinceLastSpecial;
    public bool recentlyAttackedSpecial;
    public bool isUsingSpecial;
    private float specialCooldown;

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
        timeSinceLastMeleeAttack = 0;
        timeSinceLastRangedAttack = 0;
        timeSinceLastSpecial = 0;
        isUsingSpecial = false;
        specialCooldown = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttackCooldowns();
    }

    public void CheckAttackCooldowns()
    {
        if (recentlyAttackedMelee)
        {
            if (timeSinceLastMeleeAttack >= bossMovement.meleeCooldown)
            {
                recentlyAttackedMelee = false;
                timeSinceLastMeleeAttack = 0;
            }
            else
            {
                timeSinceLastMeleeAttack += Time.deltaTime;
            }
        }

        if (recentlyAttackedRanged)
        {
            if (timeSinceLastRangedAttack >= bossMovement.rangedCooldown)
            {
                recentlyAttackedRanged = false;
                timeSinceLastRangedAttack = 0;
            }
            else
            {
                timeSinceLastRangedAttack += Time.deltaTime;
            }
        }

        if (recentlyAttackedSpecial)
        {
            if (timeSinceLastSpecial >= specialCooldown)
            {
                recentlyAttackedSpecial = false;
                timeSinceLastSpecial = 0;
            }
            else
            {
                timeSinceLastSpecial += Time.deltaTime;
            }
        }

        }

    public void MeleeAttack()
    {
        if (!recentlyAttackedMelee)
        {
           StartCoroutine(Melee());
           bossMovement.canMove = false;
        }

        bossMovement.canMove = true;

    }

    public void RangedAttack()
    {
        if (!recentlyAttackedRanged)
        {
            StartCoroutine(Ranged());
            bossMovement.canMove = false;
        }

        bossMovement.canMove = true;

    }

    public void LaunchSpecialRangedAttack()
    {
        if (!recentlyAttackedSpecial)
        {
            StartCoroutine(SpecialRanged());
            bossMovement.canMove = false;
        }

    }

    IEnumerator Melee()
    {
        recentlyAttackedMelee = true;

        // Start the melee animation

        if (!bossController.isDashing)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position + boss.transform.forward, 0.05f);
            yield return new WaitForSeconds(2f);
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position - boss.transform.forward, 0.05f);
        }
    }

    IEnumerator Ranged()
    {
        recentlyAttackedRanged = true;

        // Start the ranged animation
        Instantiate(bossMaster.projectile, boss.transform.position + boss.transform.forward, boss.transform.rotation);
        yield return new WaitForSeconds(1f);

    }

    IEnumerator SpecialRanged()
    {
        recentlyAttackedSpecial = true;
        isUsingSpecial = true;
        bossController.canDash = false;

        // Start the special animation
        for (int i = 0; i < 15; i++)
        {
            Instantiate(bossMaster.projectile, boss.transform.position + boss.transform.forward * 5, boss.transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }

        isUsingSpecial = false;
        bossController.canDash = true;
        bossMovement.canMove = true;
    }
}
