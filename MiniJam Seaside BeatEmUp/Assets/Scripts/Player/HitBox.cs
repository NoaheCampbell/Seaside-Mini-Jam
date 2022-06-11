using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    // hitbox script

    private PlayerMaster player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMaster>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // do damage
            EnemyHealth enemy = collision.gameObject.GetComponent(typeof(EnemyHealth)) as EnemyHealth;
            enemy.TakeDamage(player.meleeDmg);
        }

        if (collision.gameObject.tag == "BreakableObj")
        {
            // do damage
            BreakableObject obj = collision.gameObject.GetComponent(typeof(BreakableObject)) as BreakableObject;
            obj.HitBreakableObj(player.meleeDmg);
        }
    }
}
