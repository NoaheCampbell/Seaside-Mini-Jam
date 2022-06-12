using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed;
    public EnemyMaster enemy;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.05f;
        enemy = gameObject.GetComponent<EnemyMaster>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MeleeAnimation(Vector3 targetPos)
    {
        // Start the melee animation
        targetPosition = targetPos;
        StartCoroutine(Melee());
    }

    public void RangedAnimation()
    {
        // Start the ranged animation
        StartCoroutine(Ranged());
    }

     IEnumerator Melee()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
        yield return new WaitForSeconds(2f);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, -speed);
    }

    IEnumerator Ranged()
    {
        Instantiate(enemy.projectile);
        yield return new WaitForSeconds(1f);
    }

}
