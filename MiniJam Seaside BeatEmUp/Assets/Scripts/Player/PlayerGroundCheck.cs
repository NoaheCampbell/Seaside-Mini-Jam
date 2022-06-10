using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    // this script handles the player ground check

    private PlayerMaster player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.4f;

    void Start()
    {
        player = gameObject.GetComponent<PlayerMaster>();
    }

    // Ground Check Function - checks for layer
    void GroundCheck()
    {
        // check if grounded
        player.isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    // Update 
    void Update()
    {
        GroundCheck();
    }
}
