using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    private EnemyMaster bossMaster;
    private GameObject boss;
    private BossController bossController;
    private bool recentlyAttackedMelee;
    private bool recentlyAttackedRanged;
    private float timer;
    private float timeSinceLastAttack;
    private string lastAttack;

    // Start is called before the first frame update
    void Start()
    {
        bossMaster = gameObject.GetComponent<EnemyMaster>();
        boss = gameObject;
        bossController = gameObject.GetComponent<BossController>();
        recentlyAttackedMelee = false;
        recentlyAttackedRanged = false;
        timer = 0;
        timeSinceLastAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (recentlyAttackedMelee || recentlyAttackedRanged)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (timeSinceLastAttack > 10 && lastAttack == "melee")
        {
            recentlyAttackedMelee = false;
            timeSinceLastAttack = 0;
        }
        else if (timeSinceLastAttack > 10 && lastAttack == "ranged")
        {
            recentlyAttackedRanged = false;
            timeSinceLastAttack = 0;
        }
    }

    public void MeleeAttack()
    {
        if (!recentlyAttackedMelee)
        {
           StartCoroutine(Melee());
        }

    }

    public void RangedAttack()
    {
        if (!recentlyAttackedRanged)
        {
            StartCoroutine(Ranged());
        }

    }

    IEnumerator Melee()
    {
        recentlyAttackedMelee = true;
        lastAttack = "melee";

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
        lastAttack = "ranged";

        // Start the ranged animation
        Instantiate(bossMaster.projectile);
        yield return new WaitForSeconds(1f);
    }

}
