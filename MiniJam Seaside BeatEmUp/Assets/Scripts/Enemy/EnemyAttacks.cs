using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    private Vector3 targetPosition;
    public EnemyMaster enemy;
    

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<EnemyMaster>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RangedAnimation()
    {
        // Start the ranged animation
        StartCoroutine(Ranged());
    }

    public void DealDamage()
    {
        // Deals damage to the player
        PlayerHealth player = GameObject.FindWithTag("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
        player.TakeDamage(enemy.meleeDmg);
    }

    IEnumerator Ranged()
    {
        // Start the ranged animation
        Instantiate(enemy.projectile);
        yield return new WaitForSeconds(1f);

    }

}
