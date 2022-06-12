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
   private PlayerHealth player;
   public bool canMove;
   private bool playerHitByRay;
 
   // Start is called before the first frame update
   void Start()
   {
       boss = gameObject.GetComponent<EnemyMaster>();
       bossController = gameObject.GetComponent<BossController>();
       bossAttacks = gameObject.GetComponent<BossAttacks>();
       canRotate = true;
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
       canMove = true;
       playerHitByRay = false;
   }
 
   // Update is called once per frame
   void Update()
   {

       playerHitByRay = false;
 
       boss.transform.position = new Vector3(boss.transform.position.x, 31, boss.transform.position.z);
       
       if (boss.transform.rotation.x != 0 || boss.transform.rotation.z != 0)
       {
           boss.transform.rotation = new Quaternion(0, boss.transform.rotation.y, 0, boss.transform.rotation.w);
       }

       // Sends out raycasts to see where the player is
       for (int i = 0; i < 150; i++)
       {
           RaycastHit hit;
           Vector3 rayDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.7f, 0.7f), Random.Range(-1f, 1f));
           bool hitSomething = Physics.Raycast(transform.position, rayDirection, out hit, 100f);
 
           if (hitSomething)
           {
               if (hit.transform.gameObject.tag == "Player")
               {
                   playerHitByRay = true;
                   // If the raycast hits the player, rotate towards the ray's rotation
                   targetRotation = Quaternion.LookRotation(hit.point - transform.position);
 
                   // Gets the distance to the player and its position
                   targetDistance = Vector3.Distance(transform.position, hit.point);
                   targetPosition = hit.collider.gameObject.transform.position;
               }
 
               else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
               {
                   // Ignore the collision and the raycast keeps going
                   Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<Collider>());
               }
           }
       }
 
       // If the player is hit by a raycast, rotate towards the player
       if (playerHitByRay && canMove)
       {
           if (canRotate)
           {
                boss.transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);
           }
 
           // If the player is within range, attack
           if (targetDistance <= boss.meleeDistance)
           {
               bossAttacks.MeleeAttack();
           }
 
           if (targetDistance >= boss.minRangeDistance && targetDistance <= boss.maxRangeDistance)
           {
               bossAttacks.RangedAttack();
           }
 
           // If the boss is more than 35 units away from the player, move towards the player
           if (targetDistance > 35f)
           {
                boss.transform.position = Vector3.MoveTowards(boss.transform.position, targetPosition, boss.moveSpeed * Time.deltaTime);
           }
 
           // If the boss is under 10 units of the player, dash towards the player
           if (targetDistance < 10f)
           {
                bossController.Dash("forwards");
           }
 
           // If the player is within 20 units, dash backwards and spawn a group of minions
           if (targetDistance <= 20f)
           {
                bossController.Dash("backwards");
                bossController.SpawnMinions();
           }
 
           // If the player is within 25 units use the special attack
           if (targetDistance <= 25f)
           {
               bossAttacks.LaunchSpecialRangedAttack();
           }
       }
   }
}
 

