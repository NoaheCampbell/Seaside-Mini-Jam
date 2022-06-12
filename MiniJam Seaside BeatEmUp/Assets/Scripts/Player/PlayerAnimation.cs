using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMaster player;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerMaster>();
        controller = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForChange();
    }

    void CheckForChange()
    {
        // change animation
        AnimationChange(player.lookDirection);
    }

    void AnimationChange(float lookDir) // Up, Left, Down, Right
    {
        if (lookDir <= 3.1f && lookDir <= 0.9f)
        {
            // up
            player.movementAnimator.Play("moveup");
        }
        else if (lookDir == 1)
        {
            // right
            player.movementAnimator.Play("moveright");
        }
        else if (lookDir >= 1.1f && lookDir <= 2.9f)
        {
            // down
            player.movementAnimator.Play("movedown");
        }
        else if (lookDir == 3)
        {
            // left
            player.movementAnimator.Play("moveleft");
        }
    }
}
