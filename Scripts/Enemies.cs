using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class Enemies : MonoBehaviour
{
    public int tipeEnemyFirst;
    public int tipeEnemyFinal;
    public int numberEnemy;
    // Boolean variable that stores the state of enemiesDied
    public bool enemiesDied;
    // Teleport class instance
    public Teleport teleport;
    // Variable that stores the number of deads enemies
    public int enemiesKilled;
    // Enemy type object that will receive the skulls prefabs
    public EnemyHandler[] prefabs;
    // Spawn type object that will recieve the spawnPoints prefabs
    public GameObject[] spawnEnemies;
    // Start is called before the first frame update
    void Start()
    {
        // Assign the value false to enemiesDied
        enemiesDied = false;
        // Loop that instantiate new enemies in game
        for (int j = tipeEnemyFirst; j < tipeEnemyFinal; j++)
        {
            for (int i = 0; i < numberEnemy; i++)
            {
                // Instance GameObject that recives enemy spawn point
                GameObject spawnPoint = GetRandomSpawnPoint();
                // Local object of type Enemy responsible for instantiating the prefabs
                EnemyHandler enemy = Instantiate(this.prefabs[j], this.transform);
                // Active killed action when EnemyKilled is called
                enemy.killed += EnemyKilled;
                // Position the enemy with position parameters
                enemy.transform.localPosition = spawnPoint.transform.position;
            }
        }
    }
    // Function EnemyKilled
    public void EnemyKilled()
    {
        // Increment the number of enemies killed
        enemiesKilled++;
        // Check if all enemies already died
        if (enemiesKilled == (tipeEnemyFinal-tipeEnemyFirst)*numberEnemy)
        {
            // Assign the value true to enemiesDied
            enemiesDied=true;
        }
        // Call function isActive from class Teleport
        teleport.IsActive(enemiesDied);
    }
    // Function GetRandomSpawnPoint
    GameObject GetRandomSpawnPoint()
    {
        // Assign a random spawnPoint prefab to spawnEnemies instance
        return spawnEnemies[Random.Range(0, spawnEnemies.Length)];
    }
}
