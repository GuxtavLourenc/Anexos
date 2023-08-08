using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerHandler : MonoBehaviour
{
    // GameManager class instance
    public GameManager gameManager;
    // Boolean variable that stores the state of player life
    bool playerLive;
    // Declares the input Vector as zero
    Vector2 inputVector = Vector2.zero;
    // Declares the mouse axis Vector as zero
    Vector2 mouseVector = Vector2.zero;
    // Declare the Controller class instance
    Controller playerController;
    // Declares the boolean variable that stores the state of the player aim
    private bool aiming;
    // Start is called before the first frame update
    void Start()
    {
        // Assign the value true to playerLive
        playerLive = true;
        // Associates the Controller class instance to the Unity object Player
        playerController = GetComponent<Controller>();
        // Assigns the true value to aiming
        aiming = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Check if the mouse right button is pressed
        if (Input.GetMouseButton(1))
        {
            // Assigns to the input Vector item x the value zero
            inputVector = Vector2.zero;
            // Assigns to the mouse Vector the values of the mouse position axis
            mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Assigns the false value to aiming
            aiming = true;
        }
        else
        {
            // Assign to the input vector item x the value received from "Horizontal" input axis.
            inputVector.x = Input.GetAxis("Horizontal");
            // Assign to the input vector item y the value received from "Vertical" input axis.
            inputVector.y = Input.GetAxis("Vertical");
            // Assigns to the mouse Vector the values of the mouse position axis
            mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Assigns the true value to aiming
            aiming = false;
        }
        // Access the function "SetInputVector" of the controller class
        playerController.SetVectors(inputVector, mouseVector, aiming);
    }
    // Function called when player collides which an object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collides have the Enemy tag
        if (collision.tag == "Enemy")
        {
            // Assign the value false to playerLive
            playerLive = false;
            // Call function EndScreen from class GameManager
            gameManager.EndScreen(playerLive);
        }
    }
}
