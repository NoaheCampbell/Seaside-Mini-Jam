using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // This handles following the player

    public bool followDelay = false;

    [SerializeField] private float followSpeed;
    [SerializeField] private Transform cameraTransform;
    private Vector3 cameraPosition;

    // Update is called once per frame
    void Update()
    {
        cameraPosition = cameraTransform.position;

        if (followDelay)
        {
            var step = followSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(cameraPosition.x, transform.position.y, cameraPosition.z), step);
        }
        else
        {
            transform.position = new Vector3(cameraPosition.x, transform.position.y, cameraPosition.z);
        }
    }
}
