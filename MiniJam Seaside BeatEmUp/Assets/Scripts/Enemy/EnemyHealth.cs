using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public int maxHp = 1;
    [HideInInspector] public int health = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // function to recieve damage
    public void TakeDamage(int damage)
    {
        health -= damage;

        CheckHealth();
    }

    // function to check player health
    void CheckHealth()
    {
        if (health == 0)
        {
            Death();
        }
    }

    // function to die
    void Death()
    {
        Destroy(gameObject);
    }
}
