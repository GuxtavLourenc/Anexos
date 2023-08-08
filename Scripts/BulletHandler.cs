using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletHandler : MonoBehaviour
{
    // Boolean variable that store bullet origin
    public bool isBulletEnemy;
    // Instance of Animator class
    Animator bulletAnim;
    // Instance of Rigidbody2D class
    Rigidbody2D bulletRig;
    void Start()
    {
        // Assign the Rigidbody2D componente to the new class instance
        bulletRig = GetComponent<Rigidbody2D>();
        // Assign the Animator componente to the new class instance
        bulletAnim = GetComponent<Animator>(); 
    }
    // Function called when bullet collides which an object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Chack if origen of bullet is the enemy
        if (isBulletEnemy)
        {
            // Check if the object that collider don't have the enemy tag
            if (collision.tag != "Enemy")
            {
                // Assign the value zero to the velocity of the Rigidbody
                bulletRig.velocity = Vector2.zero;
                // Destroy bullet object after 0.12 seconds
                Destroy(gameObject, 0.12f);
            }
        }
        else
        {
            // Check if the object that collides dont't have the player tag
            if (collision.tag != "Player")
            {
                // Assign the value zero to the velocity of the Rigidbody
                bulletRig.velocity = Vector2.zero;
                // Active the trigger of Impact bullet animator parameter
                bulletAnim.SetTrigger("Impact");
                // Destroy bullet object after 0.12 seconds
                Destroy(gameObject, 0.12f);
            }
        }
    }
}