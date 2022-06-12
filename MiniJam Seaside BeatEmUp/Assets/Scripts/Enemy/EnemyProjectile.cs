using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private EnemyMaster enemy;
    public Rigidbody rb;
    private float timer;

    [HideInInspector] public float damage = 1;
    public float projectileSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<EnemyMaster>();
        transform.position = GameObject.FindWithTag("ProjectileSpawn").transform.position;
        transform.rotation = GameObject.Find("EnemyProjectileSpawn").transform.parent.rotation;
        gameObject.transform.parent = GameObject.FindWithTag("ProjectileParent").transform;
        timer = 10f;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
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
