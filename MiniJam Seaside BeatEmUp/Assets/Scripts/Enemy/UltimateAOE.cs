using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAOE : MonoBehaviour
{
    public bool active;
    public bool playerIsHit;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        playerIsHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Collision");
        if (collision.gameObject.tag == "Player")
        {  
            playerIsHit = true;
            PlayerHealth player = collision.gameObject.GetComponent(typeof(PlayerHealth)) as PlayerHealth;
            EnemyMaster enemy = gameObject.transform.parent.GetComponent(typeof(EnemyMaster)) as EnemyMaster;
            player.TakeDamage(enemy.rangedDmg);
        }
    }

}
