using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Float variable that store camera speed
    public float followSpeed;
    // Instance of Transform class
    public Transform targetPos;
    // Vector that store player position
    Vector3 playerPos;
    // Update is called once per frame
    void Update()
    {
        // Assign targetPos values to playerPos
        playerPos = new Vector3(targetPos.position.x, targetPos.position.y, -1f);
        // Assign the interpolate between camera position and player position to camera new position
        transform.position = Vector3.Slerp(transform.position, playerPos, followSpeed*Time.deltaTime);
    }
}
