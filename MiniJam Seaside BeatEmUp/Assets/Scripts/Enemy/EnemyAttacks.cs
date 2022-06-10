using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    private Vector3 _targetPosition;
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _speed = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MeleeAnimation(Vector3 targetPosition)
    {
        // Start the melee animation
        _targetPosition = targetPosition;
        StartCoroutine(Waiter());
    }

     IEnumerator Waiter()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
        yield return new WaitForSeconds(2f);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, -_speed);
    }


}
