using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMaster : MonoBehaviour
{
    // this script holds all modifiable character variables
    // since everything is dead in one hit, no need for floats as hp and damage

    [System.NonSerialized] public GameManager gameManager;

    [Header("Health")]
    public int maxHp = 1;
    public int health = 1;

    [Header("Art")]
    public GameObject playerArt;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashCooldown = 2f;
    public float jumpHeight = 1f;
    public float gravForce = -9.81f;
    public GameObject rotationObjs; // used to rotate
    [System.NonSerialized] public float lookDirection = 0; // used for debugging and firing projectiles (0 - up; 0.5 - up & right; 1 - right; 1.5 - down & right; 2 - down; 2.5 - downa nd left; 3 - left; 3.5 - up and left) 
    [System.NonSerialized] public bool canMove = true;
    [System.NonSerialized] public bool canDash = true;
    [System.NonSerialized] public bool canAttack = true;
    [System.NonSerialized] public bool isGrounded = true;

    [Header("Combat")]
    public int meleeDmg = 1;
    public float meleeDuration = 1f; //(real seconds)
    public int rangedDmg = 1;
    public float rangeCooldown = 1f;
    public GameObject meleeHitbox;
    public GameObject rangedProjectile;
    [System.NonSerialized] public bool isRecharging = false; //  recharhing ranged attack (reload)

    [Header("UI")]
    public GameObject gameOverScreen;
    public GameObject settingsMenu;
    public Slider effectsVolumeSlider;
    public Slider musicVolumeSlider;

    [Header("Animation")]
    public Animator movementAnimator;

    [Header("Audio")]
    public AudioSource effectsSource;
    public AudioSource movementSource;
    public AudioClip moveSound;
    public AudioClip dashSound;
    public AudioClip jumpSound;
    public AudioClip meleeSound;
    public AudioClip rangeSound;
    public AudioClip hurtSound;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
}
