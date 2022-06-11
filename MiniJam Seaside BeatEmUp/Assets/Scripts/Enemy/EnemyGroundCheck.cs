using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    private EnemyMaster enemy;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.4f;

    void Start()
    {
        enemy = gameObject.GetComponent<EnemyMaster>();
    }

    // Ground Check Function - checks for layer
    void GroundCheck()
    {
        // check if grounded
        enemy.isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    // Update 
    void Update()
    {
        GroundCheck();

        // If the enemy is not grounded, make the enemy fall
        if (!enemy.isGrounded)
        {
            enemy.transform.Translate(Vector3.down * -enemy.gravForce * Time.deltaTime);
        }
    }
}
