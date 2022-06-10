using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private EnemyMaster enemy;
    private Rigidbody rb;

    [HideInInspector] public float damage = 1;
    public float projectileSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<EnemyMaster>();
        transform.position = GameObject.FindWithTag("EnemyProjectileSpawn").transform.position;
        gameObject.transform.parent = GameObject.FindWithTag("ProjectileParent").transform;

        rb = gameObject.GetComponent<Rigidbody>();
        Debug.Log(rb);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.forward * projectileSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the projectile hits the player, deal damage to the player
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth player = collision.gameObject.GetComponent(typeof(PlayerHealth)) as PlayerHealth;
            player.TakeDamage(enemy.rangedDmg);
        }

        // Destroy the projectile
        Destroy(gameObject);
    }
}
