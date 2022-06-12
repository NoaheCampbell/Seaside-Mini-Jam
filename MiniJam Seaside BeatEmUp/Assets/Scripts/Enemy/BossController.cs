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

            if (timeSinceLastDash > 10)
            {
                recentlyDashed = false;
                timeSinceLastDash = 0;
            }
        }

    }

    public void Dash(string direction)
    {
        if (!recentlyDashed && bossMaster.isGrounded && canDash)
        {   
            Debug.Log("Dashing");

            // Start the jump animation
            StartCoroutine(DashAnimation(direction));
        }
    }

    public void Spin()
    {
        StartCoroutine(SpinAnimation());
    }

    public void SpawnMinions()
    {
        // Spawn minions (not implemented)
    }

    IEnumerator DashAnimation(string direction)
    {
        recentlyDashed = true;
        isDashing = true;

        // Makes the boss dash towards the player
        var timerLeft = 50f;

        // Backs up a little bit before dashing forward
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position - boss.transform.forward, bossMaster.moveSpeed);

        bossMovement.canRotate = false;

        if (direction == "forward")
        {
            while (timerLeft > 0)
            {
            // Moves the boss forward in a dash, unless they get more than 20 units from the player
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position + boss.transform.forward, bossMaster.dashSpeed);
            yield return new WaitForSeconds(0.01f);

            if (bossMovement.targetDistance > 20)
            {
                bossMovement.canRotate = true;
                yield break;
            }

            timerLeft -= Time.deltaTime;
            }
        }

        else if (direction == "backwards")
        {
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

                timerLeft -= Time.deltaTime;
            }
        }

        bossMovement.canRotate = true;
        isDashing = false;
    }

    IEnumerator SpinAnimation()
    {
        // Makes the boss spin around for a few seconds
        var timerLeft = 5f;
        bossMovement.canRotate = false;

        while (timerLeft > 0)
        {
            boss.transform.rotation = Quaternion.RotateTowards(boss.transform.rotation, Quaternion.Euler(0, boss.transform.rotation.y + 360, 0), bossMaster.rotationSpeed);
            yield return new WaitForSeconds(0.01f);
            timerLeft -= 0.5f;
        }

        bossMovement.canRotate = true;
    }

}
