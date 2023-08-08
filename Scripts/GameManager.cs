using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Bollean variable that store state of level
    public bool isFinalLevel;
    // Integer variable that stores the game level
    public int level;
    // Canvas object class instance
    public GameObject canvas;
    // Win object class instance
    public GameObject win;
    // Lose object class instance
    public GameObject lose;
    // Boolean variable that store winner state
    bool winner;
    // Float variable that store the time
    float time = 0;
    // Boolean variable that store endScreen state
    bool endScreen;
    void Start()
    {
        // Restart game
        Time.timeScale = 1;
        // Assign value false to winner
        winner = false;
        // Assign value false to endScreen
        endScreen = false;
        // Disable the instance object canvas
        canvas.SetActive(false);
        // Disable the instance object win
        win.SetActive(false);
        // Disable the instance object lose
        lose.SetActive(false);
    }
    void Update()
    {
        // Check if endScreen is true
        if (endScreen)
        {
            // Start counting time
            time += 0.016f;
            // Check if time is bigger than 5
            if (time > 3.0f)
            {
                if (isFinalLevel && winner)
                {
                    // Call scene Menu
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    // Call scene Level
                    SceneManager.LoadScene("Level" + level);
                }
            }
        }
    }
    // Function EndScreen
    public void EndScreen(bool youWin)
    {
        // Stop game
        Time.timeScale = 0;
        // Assign the value of youWin to winner
        winner = youWin;
        // Assign the value 0 to time
        time = 0;
        // Assign the state true to endScreen
        endScreen = true;
        // Active the instace object canvas
        canvas.SetActive(true);
        // Check if winner is true
        if (winner)
        {
            // Active the instance object win
            win.SetActive(true);
            // Disable the instance object lose
            lose.SetActive(false);
            // Increase level
            level++;
        }
        else
        {
            // Disabe the instance object win
            win.SetActive(false);
            // Active the instance object lose
            lose.SetActive(true);
        }
    }
}