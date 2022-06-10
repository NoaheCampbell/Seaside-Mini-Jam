using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // manages player health

    private PlayerMaster player;
    private bool respawning = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMaster>();
    }

    // function to recieve damage
    public void TakeDamage(int damage)
    {
        player.health -= damage;
        Debug.Log("take damage");

        CheckHealth();
    }

    // function to check player health
    void CheckHealth()
    {
        Debug.Log("Check Health");
        if (player.health == 0)
        {
            Death();
        }
    }

    // function to die
    void Death()
    {
        Debug.Log("Died");

        // restrict player movement
        player.canMove = false;

        // remove a life
        player.lives -= 1;

        // check if out of lives
        if (player.lives <= 0)
        {
            // TODO
        }
        else
        {
            StartCoroutine(Respawn());
        }
    }

    // respawn 
    IEnumerator Respawn()
    {
        respawning = true;

        // turn off player art
        player.playerArt.SetActive(false);

        // teleport player
        player.transform.position = GameObject.FindWithTag("Respawn").transform.position;

        yield return new WaitForSeconds(1f);

        player.health = player.maxHp;

        // turn art on
        player.playerArt.SetActive(true);

        player.canMove = true;

        respawning = false;
    }
}
