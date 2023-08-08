using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    // Vector that store bullet direction
    Vector2 bulletDir;
    // Float variable that store bullet speed 
    public float bulletSpeed;
    // Float variable that store bullet angle
    float bulletAngle;
    // Float variable that store muzze angle
    float muzzeAngle;
    // Vector that store muzze position
    Vector2 muzzlePosition;
    // Animator class instance
    Animator bulletAnim;
    // Bullets Prefabs class instance
    public GameObject bulletPrefab;
    void Start()
    {
        // Assign the animator component to the new class instance
        bulletAnim = GetComponent<Animator>();
    }
    // Function that control all bullets
    public void fireBullet(Vector2 aimDir, Vector2 aim)
    {
        // Rotate the object to your original angle
        transform.rotation = Quaternion.identity;
        // Calcule aim angle e assign your value to bulletAngle 
        bulletAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        // Check if bulletAngle is less than 0
        if (bulletAngle < 0)
        {
            // add 360 to bulletAngle
            bulletAngle += 360;
        }
        // Define muzzleAngle
        for (float i = 22.5f; i < 337.5f; i+=45.0f)
        {
            // Check which quadrant bulletAngle belongs
            if (bulletAngle > i && bulletAngle < i+45.0f)
            {
                // Define muzzeAngle
                muzzeAngle = i + 22.5f;
                // Loop break
                break;
            }
            else
            {
                // Define muzzleAngle
                muzzeAngle = 0;
            }
        }
        // Variable that define muzze angle quadrant
        float quadrant = muzzeAngle / 45.0f;
        // Check if muzze quadrant is on the right 
        if (quadrant == 7 || quadrant == 0 || quadrant == 1)
        {
            // Assign the value 0.45 to parameter x of muzzePosition
            muzzlePosition.x = 0.45f;
        }
        // Check if muzze quadrant is on the left
        else if (quadrant >= 3 && quadrant <= 5)
        {
            // Assign the value -0.45 to parameter x of muzzePosition
            muzzlePosition.x = -0.45f;
        }
        // It means that muzze quadrant is on the mid 
        else
        {
            // Assign the value 0 to parameter x of muzzePosition
            muzzlePosition.x = 0;
        }
        // Check if muzze quadrant is on the top
        if (quadrant >=1 && quadrant <= 3)
        {
            // Assign the value 0.65 to parameter y of muzzePosition
            muzzlePosition.y = 1.25f;
        }
        // Check if muzze quadrant is on the top
        else if (quadrant >= 5 && quadrant <=7)
        {
            // Assign the value -0.4 to parameter y of muzzePosition
            muzzlePosition.y = 0.15f;
        }
        // It means that muzze quadrant is on the mid 
        else
        {
            // Assign the value 0.15 to parameter y of muzzePosition
            muzzlePosition.y = 0.65f;
        }
        // Rotate the object to muzzleAngle
        transform.Rotate(0f, 0f, muzzeAngle);
        // Position the object in muzzlePosition
        transform.localPosition = muzzlePosition;
        // Assign the direction of aim to bulletDir
        bulletDir = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
        // Make bulletDir have the magnitude of 1
        bulletDir.Normalize();
        // Active the Fire trigger animator parameter
        bulletAnim.SetTrigger("Fire");
        // Create a new instance of the bullet object
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // Change the bullet angle to match aim angle
        bullet.transform.Rotate(0f, 0f, bulletAngle);
        // Assign the velocity of the Rigidbody2D componente the bullet direction and speed
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDir * bulletSpeed;
    }
}
