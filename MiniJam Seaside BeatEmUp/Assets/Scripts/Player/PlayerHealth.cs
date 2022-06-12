using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // manages player health

    private PlayerMaster player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMaster>();
    }

    // function to recieve damage
    public void TakeDamage(int damage)
    {
        player.health -= damage;
        //Debug.Log("take damage");

        CheckHealth();
    }

    // function to check player health
    void CheckHealth()
    {
        //Debug.Log("Check Health");
        if (player.health == 0)
        {
            Death();
        }
    }

    // function to die
    void Death()
    {
        player.gameManager.playerLives -= 1;
        player.gameManager.RespawnPlayer();
    }

}
