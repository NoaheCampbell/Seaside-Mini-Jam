using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private EnemyMaster bossMaster;
    private GameObject boss;
    public bool recentlyJumped;
    private float timer;
    private Vector3 bossPosition;
    private float timeSinceLastJump;

    // Start is called before the first frame update
    void Start()
    {
        bossMaster = gameObject.GetComponent<EnemyMaster>();

        // Changes enemymaster fields to match boss
        bossMaster.EnemyType(gameObject.tag);

        boss = gameObject;
        recentlyJumped = false;
        timer = 0;
        timeSinceLastJump = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (recentlyJumped)
        {
            timeSinceLastJump += Time.deltaTime;
        }

        if (timeSinceLastJump > 10)
        {
            recentlyJumped = false;
            timeSinceLastJump = 0;
        }

    }

    public void Jump()
    {
        bossPosition = boss.transform.position;

        if (!recentlyJumped && bossMaster.isGrounded)
        {
            recentlyJumped = true;
            
            // Start the jump animation
            StartCoroutine(JumpAnimation());
        }
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

    }
    
}
