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

        CheckHealth();
    }

    // function to check player health
    void CheckHealth()
    {
        if (player.health == 0)
        {
            Death();
        }
    }

    // function to die
    void Death()
    {
        // remove a life
        player.lives -= 1;

        // check if out of lives
        if (player.lives <= 0)
        {
            // TODO
        }

        // respawn player
        player.transform.position = GameObject.FindWithTag("Respawn").transform.position;
    }
}
