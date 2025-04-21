using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages the spawning of enemy vehicles in the simulation environment.
/// Handles timing, positioning, and randomization of obstacles.
/// </summary>
public class SpawnManager : MonoBehaviour
{
    /// <summary>
    /// Prefab of the enemy car to spawn
    /// </summary>
    public GameObject Car_enemy;
    
    /// <summary>
    /// Time interval between spawn waves in seconds
    /// </summary>
    public float spawnInterval = 4f;
    
    private float timer = 0f;

    /// <summary>
    /// X positions of available lanes for spawning
    /// </summary>
    float[] lanesX = new float[] { -1.5f, -0.5f, 0.5f };

    /// <summary>
    /// Updates the spawn timing and creates new enemy vehicles according to the defined interval.
    /// Randomly selects lanes and ensures no lane receives multiple cars in a single spawn cycle.
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            int nbCarsToSpawn = Random.Range(1, 3);

            List<int> availableLanes = new List<int> { 0, 1, 2 };

            for (int i = 0; i < nbCarsToSpawn; i++)
            {
                int laneIndex = availableLanes[Random.Range(0, availableLanes.Count)];
                availableLanes.Remove(laneIndex);

                Vector3 spawnPosition = new Vector3(lanesX[laneIndex], 7f, 0f);
                Instantiate(Car_enemy, spawnPosition, Quaternion.identity);
            }
        }
    }
}
