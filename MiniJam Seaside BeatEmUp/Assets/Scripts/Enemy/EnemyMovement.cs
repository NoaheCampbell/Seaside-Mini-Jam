using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Quaternion _targetRotation;
    private Vector3 _targetPosition;
    private float _speed;
    public bool playerIsHit;
    private string _objectTag;
    public float distance;
    private EnemyAttacks _enemyAttacks;
    private float _timer;
    public float attackDuration;

    // Start is called before the first frame update
    void Start()
    {
        _enemyAttacks = GetComponent<EnemyAttacks>();
        _speed = 0.05f;
        _timer = 0;
        attackDuration = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Sends out raycasts to determine if the player is near the enemy
        for (int i = 0; i < 100; i++)
        {            

            RaycastHit hit;
            Vector3 rayDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            bool hitSomething = Physics.Raycast(transform.position, rayDirection, out hit, 200f);

            if (hitSomething)
                _objectTag = hit.transform.gameObject.tag;

            if (hitSomething && _objectTag == "Player")
            {
                // If the raycast hits the player, rotate towards the ray's rotation
                _targetRotation = Quaternion.LookRotation(hit.point - transform.position);

                // Gets the distance to the player and its position
                distance = Vector3.Distance(transform.position, hit.point);
                _targetPosition = hit.collider.gameObject.transform.position;

                // Turns playerIsHit to true for the rest of the loop
                playerIsHit = true;
            }
            else
            {
                if (i == 0)
                {
                    // If the raycasts didn't hit the player, keep the enemy still
                    _targetRotation = transform.rotation;
                    _targetPosition = transform.position;
                    playerIsHit = false;
                }

            }
        }
        
        // If playerIsHit is true, go through multiple checks to see how the enemy should move
        if (playerIsHit)
        {
            // If the enemy is under 30 units away from the player, move closer to the player
            if (distance < 30f && distance > 5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);
            }

            // Creates a random number between 0 and 1 to determine if the enemy should attack
            float randomNumber = Random.Range(0f, 1f);
            if (distance <= 5f && randomNumber < 0.25f)
            {
                _enemyAttacks.MeleeAnimation(_targetPosition);

                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, -_speed * 2);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);
            }
            else if (distance <= 5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, -_speed * 400);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);
            }

            // If the enemy is between 30 and 50 units from the player, back away even more (possible switch to ranged mode later)
            if (distance > 30f && distance < 50f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, -_speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);
            }

            // If the enemy is more than 80 units from the player, move towards the player
            if (distance > 80f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);
            }
        }

        _timer += Time.deltaTime;
    }
}

       
