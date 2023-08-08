using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Function that start the game
    public void StartGame()
    {
        // Load scene "Game"
        SceneManager.LoadScene("Level1");
    }
    // Function that close the game
    public void Quit()
    {
        // Close Unity application
        Application.Quit();
    }
}
