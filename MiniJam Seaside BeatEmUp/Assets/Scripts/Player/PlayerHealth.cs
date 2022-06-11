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
        // Debug.Log("take damage");

        CheckHealth();
    }

    // function to check player health
    void CheckHealth()
    {
        // Debug.Log("Check Health");
        if (player.health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    // function to die
    IEnumerator Death()
    {
        // Debug.Log("Died");
        // remove a life
        player.lives -= 1;

        // check if out of lives
        if (player.lives <= 0)
        {
            // TODO
        }

        // respawn player
        player.canMove = false;
        yield return new WaitForSeconds(1f);
        player.transform.position = GameObject.FindWithTag("Respawn").transform.position;
        player.canMove = true;
        player.health = player.maxHp;
    }
}
