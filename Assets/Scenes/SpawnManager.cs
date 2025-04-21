using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public GameObject Car_enemy;
    public float spawnInterval = 4f;
    private float timer = 0f;

    float[] lanesX = new float[] { -1.5f, -0.5f, 0.5f };

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
