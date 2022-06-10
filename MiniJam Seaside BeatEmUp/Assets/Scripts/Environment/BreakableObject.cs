using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    public int maxHp = 3;
    private int health = 3;

    // hit obj
    public void HitBreakableObj(int damage)
    {
        health -= damage;
        ChangeState();
    }

    // change objs state
    void ChangeState()
    {
        // change visually

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
