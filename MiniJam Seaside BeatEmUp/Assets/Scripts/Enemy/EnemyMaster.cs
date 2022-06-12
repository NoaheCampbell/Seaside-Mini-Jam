using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{

    // Holds all enemy information related to health
   [Header("Health")]
    public int maxHp = 1;
    public int health = 1;
    public bool canBeDamaged = true;

    // Holds all enemy information related to movement, direction, and the ability to attack
    [Header("Movement")]
    public float moveSpeed = 10f;
    public bool isGrounded = true;
    public float gravForce = -1f; 
    public float dashSpeed;
    public float rotationSpeed = 30f;

    // Holds all enemy information related to combat hitboxes, damage, and cooldowns
    [Header("Combat")]
    public int meleeDmg = 5;
    public int rangedDmg = 1;
    public GameObject projectile;
    public string combatPreference;
    public float meleeDistance = 3f;

    public void EnemyType(string tag)
    {
        if (tag == "Boss")
        {
            meleeDmg = 1;
            rangedDmg = 1;
            canBeDamaged = false;
            dashSpeed = 4f;
        }
        
    }

    public void DetermineCombatType()
    {
        if (gameObject.name.Contains("Melee"))
        {
           combatPreference = "melee";
        }
        else if (gameObject.name.Contains("Ranged"))
        {
            combatPreference = "ranged";
        }
        else
        {
           combatPreference = "hybrid";
        }
    }
}
