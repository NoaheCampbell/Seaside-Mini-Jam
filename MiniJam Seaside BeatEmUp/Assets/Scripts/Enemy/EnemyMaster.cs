using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{

    // Holds all enemy information related to health
   [Header("Health")]
    public int maxHp = 1;
    public int health = 1;

    // Holds all enemy information related to movement, direction, and the ability to attack
    [Header("Movement")]
    public float moveSpeed = 2f;

    // Holds all enemy information related to combat hitboxes, damage, and cooldowns
    [Header("Combat")]
    public int meleeDmg = 5;
    public float meleeDuration = 1f;
    public int rangedDmg = 1;
    public float rangeCooldown = 0.5f;
    public GameObject projectile;
}
