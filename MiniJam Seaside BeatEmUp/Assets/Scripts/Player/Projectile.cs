using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    // projectile script

    private PlayerMaster player;
    private Rigidbody rb;
    private Vector3 direction;

    [HideInInspector] public float damage = 1;
    public float projectileSpeed = 1;

    public GameObject projectileArt;
    public GameObject projectileVertArt;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMaster>();
        transform.position = GameObject.FindWithTag("ProjectileSpawn").transform.position;
        transform.localRotation = GameObject.FindWithTag("Player").GetComponent<PlayerMaster>().rotationObjs.transform.localRotation;
        gameObject.transform.parent = GameObject.FindWithTag("ProjectileParent").transform;

        rb = gameObject.GetComponent<Rigidbody>();

        GetDirection();
    }

    private void Update()
    {
        rb.velocity = direction * projectileSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            // do damage
            EnemyHealth enemy = collision.gameObject.GetComponent(typeof(EnemyHealth)) as EnemyHealth;
            enemy.TakeDamage(player.rangedDmg);

        }

        if (collision.gameObject.tag == "BreakableObj")
        {
            // do damage
            BreakableObject obj = collision.gameObject.GetComponent(typeof(BreakableObject)) as BreakableObject;
            obj.HitBreakableObj(player.rangedDmg);
        }

        // destroy object
        Destroy(gameObject);
    }

    // get player direction (where to shoot)
    void GetDirection()
    {
        // check which way player is facing

        if (player.lookDirection == 0)
        {
            // shoot up
            direction = new Vector3(0, 0, 1);

            projectileArt.SetActive(false);
            projectileVertArt.SetActive(true);
        }
        else if (player.lookDirection == 0.5f)
        {
            // shoot up & right
            direction = new Vector3(1, 0, 1);
        }
        else if (player.lookDirection == 1)
        {
            // shoot right
            direction = new Vector3(1, 0, 0);
        }
        else if (player.lookDirection == 1.5f)
        {
            // shoot down & right
            direction = new Vector3(1, 0, -1);
        }
        else if (player.lookDirection == 2)
        {
            // shoot down
            direction = new Vector3(0, 0, -1);

            projectileArt.SetActive(false);
            projectileVertArt.SetActive(true);
        }
        else if (player.lookDirection == 2.5f)
        {
            // shoot down & left
            direction = new Vector3(-1, 0, -1);
        }
        else if (player.lookDirection == 3)
        {
            // shoot left
            direction = new Vector3(-1, 0, 0);
        }
        else if (player.lookDirection == 3.5f)
        {
            // shoot up & left
            direction = new Vector3(-1, 0, 1);
        }
    }
}
