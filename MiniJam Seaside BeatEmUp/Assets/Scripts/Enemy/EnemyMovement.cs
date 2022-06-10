using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Quaternion _targetRotation;
    private Vector3 _targetPosition;
    private float _speed = 0.05f;
    public bool playerIsHit;
    private string _objectTag;
    private float _distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Sends out raycasts to determine if the player is near the enemy
        for (int i = 0; i < 200; i++)
        {            

            RaycastHit hit;
            Vector3 rayDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            bool hitSomething = Physics.Raycast(transform.position, rayDirection, out hit, 500f);

            // Draws raycasts in the same place as the random raycasts
            Debug.DrawRay(transform.position, rayDirection * 500f, Color.red);

            if (hitSomething)
                _objectTag = hit.transform.gameObject.tag;

            if (hitSomething && _objectTag == "Player")
            {
                // If the raycast hits the player, rotate towards the ray's rotation
                _targetRotation = Quaternion.LookRotation(hit.point - transform.position);

                // Gets the distance to the player and its position
                _distance = Vector3.Distance(transform.position, hit.point);
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
            // If the enemy is within 30 units of the player, move closer to the player
            if (_distance < 30f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);
            }

            // If the enemy is less than 5 units from the player, move backwards
            if (_distance < 5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, -_speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);
            }

            // If the enemy is between 30 and 50 units from the player, back away even more (possible switch to ranged mode later)
            if (_distance > 30f && _distance < 50f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, -_speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);
            }

            // If the enemy is more than 80 units from the player, move towards the player
            if (_distance > 80f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);
            }

            // 
        }
            
    }
}

       
