using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // This Script handles all player input/controls

    private CharacterController controller;
    private PlayerMaster player;
    
    private Vector3 playerVelocity; // velocity up/down
    private Vector3 move;

    private bool moving = false;
    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        player = gameObject.GetComponent<PlayerMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        MeleeAttack();
        RangedAttack();
    }


    #region Attacks

    // melee attack
    void MeleeAttack()
    {
        if (player.canAttack && !attacking)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(Melee());
            }
        }
    }

    // melee coroutine
    IEnumerator Melee()
    {
        attacking = true;
        player.canMove = false;
        player.meleeHitbox.SetActive(true);
        yield return new WaitForSeconds(player.meleeDuration);
        player.meleeHitbox.SetActive(false);
        player.canMove = true;
        attacking = false;
    }

    // ranged attack
    void RangedAttack()
    {
        if (player.canAttack)
        {
            if (Input.GetButtonDown("Fire2") && !player.isRecharging)
            {
                // spawn projectile
                Instantiate(player.rangedProjectile);

                // start recharge coroutine
                StartCoroutine(RangedRecharge());
            }
        }
    }

    // ranged attack recharge
    IEnumerator RangedRecharge()
    {
        player.isRecharging = true;
        yield return new WaitForSeconds(player.rangeCooldown);
        player.isRecharging = false;
    }

    #endregion


    #region Movement

    // move function
    void Move()
    {
        if (player.canMove)
        {
            //reset velocity
            if (player.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            // get input
            move = player.transform.right * Input.GetAxis("Horizontal") + player.transform.forward * Input.GetAxis("Vertical");

            // normalize
            if (move.x > 1 || move.x < -1 || move.y > 1 || move.y < -1 || move.z > 1 || move.z < -1)
            {
                move = move.normalized;
            }

            // rotate player
            Rotation();

            // move
            controller.Move(move * Time.deltaTime * player.moveSpeed);

            if (move.x != 0 || move.y != 0 || move.z != 0)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
        }
    }

    // Jump Function
    void Jump()
    {
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && player.isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(player.jumpHeight * -3.0f * player.gravForce);
        }

        // apply gravity 
        playerVelocity.y += player.gravForce * Time.deltaTime;

        // move
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // rotate player
    void Rotation()
    {
        if (moving)
        {
            // get which direction to face

            if (move.z > 0 && move.x == 0)
            {
                // Up direction
                player.rotationObjs.transform.rotation = Quaternion.Euler(0, -90, 0);

                // set direction
                player.lookDirection = 0;
            }
            else if (move.z > 0 && move.x > 0)
            {
                // Up & right direction
                player.rotationObjs.transform.rotation = Quaternion.Euler(0, -45, 0);

                // set direction
                player.lookDirection = .5f;
            }
            else if (move.z == 0 && move.x > 0)
            {
                // right direction
                player.rotationObjs.transform.rotation = Quaternion.Euler(0, 0, 0);

                // set direction
                player.lookDirection = 1f;
            }
            else if (move.z < 0 && move.x > 0)
            {
                // down & right direction
                player.rotationObjs.transform.rotation = Quaternion.Euler(0, 45, 0);

                // set direction
                player.lookDirection = 1.5f;
            }
            else if (move.z < 0 && move.x == 0)
            {
                // down direction
                player.rotationObjs.transform.rotation = Quaternion.Euler(0, 90, 0);

                // set direction
                player.lookDirection = 2f;
            }
            else if (move.z < 0 && move.x < 0)
            {
                // down & left direction
                player.rotationObjs.transform.rotation = Quaternion.Euler(0, 135, 0);

                // set direction
                player.lookDirection = 2.5f;
            }
            else if (move.z == 0 && move.x < 0)
            {
                // left direction
                player.rotationObjs.transform.rotation = Quaternion.Euler(0, 180, 0);

                // set direction
                player.lookDirection = 3f;
            }
            else if (move.z > 0 && move.x < 0)
            {
                // up & left direction
                player.rotationObjs.transform.rotation = Quaternion.Euler(0, -135, 0);

                // set direction
                player.lookDirection = 3.5f;
            }
        }
    }

    #endregion
}
