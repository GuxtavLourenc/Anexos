using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Controller : MonoBehaviour
{
    // Float variable that store bullet reload time 
    float reloadTime;
    // Bullet class instance;
    Bullet bulletParameters;
    // Vector that store player position
    Vector2 playerPosition;
    // Vector that store the aim direction
    Vector2 aimDirection;
    // Variable that store the Unity object velocity
    public float velocity = 5;
    // Vector that store the Unity object movement direction
    Vector2 movement;
    // Vector that store the aim position
    Vector2 aim;
    // Declare the boolean variable that store the state of the player aim
    bool isAiming;
    // Rigidbody2D class instance
    Rigidbody2D bodyRig;
    // Animator class instance
    Animator anim;
    // Crosshair object instance
    public GameObject crossHair;
    // Start is called before the first frame update
    void Start()
    {
        // Set value of reloadTime to 3
        reloadTime = 2.0f;
        // Associate the bullet class instance to the Unity object component
        bulletParameters = GetComponentInChildren<Bullet>();
        // Associate the Rigidbody2D class instance to the Unity object component
        bodyRig = GetComponent<Rigidbody2D>();
        // Associate the Animator class instance to the Unity object component
        anim = GetComponentInChildren<Animator>();
        // Disable object crossHair
        crossHair.SetActive(false);
        // Make mouse cursor invisible
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Increase reloadTime value over time
        reloadTime += Time.deltaTime;
        // Assign player position to playerPosition variable
        playerPosition = transform.position;
        // Check if isAiming is true
        if(isAiming)
        {
            // Call the function Aiming
            Aiming();
        }
        else
        {
            // Call the function Movement
            Movement();
        }
    }
    // Function Movement
    void Movement()
    {
        // Disable crossHair object 
        crossHair.SetActive(false);
        // checks if the movement is diagonal
        if (movement.x != 0 && movement.y != 0)
        {
            // Assign the values of movement.x and movement.y to the velocity field of Rigidbody2D
            bodyRig.velocity = new Vector2(movement.x, movement.y) * velocity * 0.7f;
        }
        else
        {
            // Assign the values of movement.x and movement.y to the velocity field of Rigidbody2D
            bodyRig.velocity = new Vector2(movement.x, movement.y) * velocity;
        }
        // Assign the value salse to the Aim animator parameter
        anim.SetBool("Aim", false);
        // Assign the values of movement.x to the Horizontal animator parameter
        anim.SetFloat("Horizontal", movement.x);
        // Assign the values of movement.x to the Vertical animator parameter
        anim.SetFloat("Vertical", movement.y);
        // Assign the values of magnitude to the Magnitude animator parameter
        anim.SetFloat("Magnitude", movement.magnitude);
    }
    // Function SetVectors
    public void SetVectors(Vector2 inputVector, Vector2 mouseVector, bool aiming)
    {
        // Assign the value of item x of inputVector vector to item x of the movement vector
        movement.x = inputVector.x;
        // Assign the value of item y of inputVector vector to item y of the movement vector
        movement.y = inputVector.y;
        // Assign  the values of mouse Vector to aim Vector
        aim = mouseVector;
        // Assign the value of aiming to isAiming
        isAiming = aiming;
    }
    // Function Aiming
    public void Aiming()
    {
        // Assign the value of the subtracion between aim and playerPosition to aimDirection
        aimDirection = aim - playerPosition;
        // Change magnitude of aimDirection to 1
        aimDirection.Normalize();
        // Active the object crossHair
        crossHair.SetActive(true);
        // Turn the velocity of the player RigidBody to zero
        bodyRig.velocity = Vector2.zero;
        // Assign the value true to Aim animator parameter
        anim.SetBool("Aim", true);
        // Assign the value of aimDirection.x to AimHorizontal animator parameter
        anim.SetFloat("AimHorizontal", aimDirection.x);
        // Assign the value of aimDirection.y to AimHorizontal animator parameter
        anim.SetFloat("AimVertical", aimDirection.y);
        // Assign the values of aim to crossHair position
        crossHair.transform.position = aim;
        // Check if mouse left button is pressed
        if (Input.GetMouseButtonDown(0) && reloadTime >= 3)
        {
            // Resets reload time value
            reloadTime = 0.0f;
            // Call function fireBullet by bulletParameter instance with aimDirection and aim parameters
            bulletParameters.fireBullet(aimDirection, aim);
        }
    }
}