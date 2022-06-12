using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float timer;
    private int rangeDmg;
    public float projectileSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rangeDmg = 1;

        transform.position = gameObject.transform.parent.transform.position;
        transform.rotation = gameObject.transform.parent.transform.rotation;

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
        if (collision.gameObject.tag == "HurtBox" || collision.gameObject.tag == "Player" || collision.gameObject.transform.parent.tag == "Player")
        {
            PlayerHealth player = GameObject.FindWithTag("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
            player.TakeDamage(rangeDmg);

            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        else 
        {
            Debug.Log(collision.gameObject.transform.parent.tag);
        }
    }
}
