using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyHandler : MonoBehaviour
{
    // Bullet class instance;
    Bullet bulletParameters;
    // Vector that stores random numbers
    private Vector2 randomNum;
    // Float variable that store walk time
    float walkTime;
    // Boolean variable that stores walk state
    public bool hasRandomWalk;
    // Vector that store random position
    private Vector2 randomPos;
    // Boolean variable that store the type of enemy moviment
    public bool hasWalkAnim;
    // Animator class instance
    Animator anim;
    // Enemy death action
    public System.Action killed;
    // Instance of Transform class
    Transform playerPos;
    // Float variable that store enemy speed
    public float enemySpeed;
    // Instance of Rigidbody2D class
    Rigidbody2D enemyRig;
    // Vector that store target direction
    Vector2 targetDir;
    void Start()
    {
        // Associate the bullet class instance to the Unity object component
        bulletParameters = GetComponentInChildren<Bullet>();
        // Assign random integer numbers to randomNum verctor
        randomNum = new Vector2((int)Mathf.Round(Random.Range(-1.0f, 1.0f)), (int)Mathf.Round(Random.Range(-1.0f, 1.0f)));
        // Set value of walkTime to 1
        walkTime = 0.0f;
        // Assign the Animator component to the new class instance
        anim = GetComponentInChildren<Animator>();
        // Assign the Transform component with tag "Player" to the new class instance
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // Assign the Rigidbody2D component to the new class instance
        enemyRig = GetComponent<Rigidbody2D>();
        // Assign to randomPos a new position based in your actual position and random numbers
        randomPos = new Vector2(this.transform.position.x + randomNum.x, this.transform.position.y + randomNum.y);
    }
    void Update()
    {
        // Increase walkTime value over time
        walkTime += Time.deltaTime;
        // Calculate the vector between player position and enemy position
        targetDir = playerPos.position - this.transform.position;
        // Add 0.5 to y parameter of targetDir
        targetDir.y += 0.5f;
        // Make targetDir have a magnitude of 1
        targetDir.Normalize();
        // Check if hasWalkAnim is true
        if (hasWalkAnim)
        {
            // Check if walkTime is equal or bigger than 1
            if (hasRandomWalk && (walkTime >= 2))
            {
                Fire();
                // Reset walkTime
                walkTime = 0.0f;
                // Assign random integer numbers to randomNum verctor
                randomNum = new Vector2((int)Mathf.Round(Random.Range(-1.0f, 1.0f)), (int)Mathf.Round(Random.Range(-1.0f, 1.0f)));
                // Assign to randomPos a new position based in your actual position and random numbers
                randomPos = new Vector2(this.transform.position.x + randomNum.x, this.transform.position.y + randomNum.y);
            }
            // Call function walk
            Walk();
        }
        else
        {
            // Call function follow
            Follow();
        }
    }
    // Function called when Enemy collides which an object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if Invader collided with an object that has the Bullet tag
        if (collision.tag == "Bullet")
        {
            // Call action killed
            this.killed.Invoke();
            // Disable object Enemy
            this.gameObject.SetActive(false);
        }
    }
    // Function Follow
    void Follow()
    {
        // Check if enemy velocity is less than 5
        if (enemyRig.velocity.magnitude <= 5.0f)
        {
            // Add force to the enemy body
            enemyRig.AddForce(targetDir * enemySpeed);
        }
    }
    //Function Walk
    void Walk()
    {
        // Check if hasRandomWalk is true
        if (hasRandomWalk)
        {
            // Assign the direction of the new position to targetDir
            targetDir = new Vector2(randomPos.x - this.transform.position.x, randomPos.y - this.transform.position.y);
            // Change magnitude of values to 1
            targetDir.Normalize();
            // Zero rigidbody velocity
            enemyRig.velocity = Vector2.zero;
            // Assign the values of targertDir and enemySpeed to the rigidbody2D of enemy
            enemyRig.velocity = new Vector2(targetDir.x, targetDir.y) * enemySpeed;
        }
        // Assign the values of targetDir.x to the Horizontal animator parameter
        anim.SetFloat("Horizontal", targetDir.x);
        // Assign the values of targetDir.x to the Vertical animator parameter
        anim.SetFloat("Vertical", targetDir.y);
        // Assign the values of targetDir.magnitude to the Magnitude animator parameter
        anim.SetFloat("Magnitude", targetDir.magnitude);
    }
    void Fire()
    {
        // Call function fireBullet by bulletParameter instance with aimDirection and aim parameters
        bulletParameters.fireBullet(targetDir, playerPos.position);
    }
}
