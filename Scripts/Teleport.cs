using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Teleport : MonoBehaviour
{
    // GameManager instance class
    public GameManager gameManager;
    // Boolean variable that store the winner state
    bool winner;
    // CircleCollider class instance
    CircleCollider2D circleCollider;
    // Bollean variable that store isActive state
    bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        // Assign the state false to winner
        winner = false;
        // Assign the state false to isActive
        isActive = false;
        // Associate the CircleCollider2D class instance to the Unity object component
        circleCollider = GetComponent<CircleCollider2D>();
        // Disable the component circleCollider
        circleCollider.enabled = false;
    }
    // Function IsActive
    public void IsActive(bool active)
    {
        // Assign the state of active to isActive
        isActive = active;
        // Check if the state of isActive is true
        if (isActive)
        {
            // Active the component circleCollider
            circleCollider.enabled = true;
            // Assign the state true to winner
            winner = true;
        }
    }
    // Function called when Teleport collides with an object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if this object have the tag Player and if the state of isActive is true
        if ((isActive) && (collision.tag == "Player"))
        {
            // Call function EndScreen from class GameManager
            gameManager.EndScreen(winner);
        }
    }
}
