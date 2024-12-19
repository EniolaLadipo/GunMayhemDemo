using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;  // Array of power-up prefabs to spawn
    public Transform[] spawnPoints;      // Predefined spawn points for power-ups
    private List<GameObject> activePowerUps = new List<GameObject>();  // List to track active power-ups
    private const int MAX_ACTIVE_POWERUPS = 2; // Maximum number of power-ups on screen at once
    private const float MIN_RESPAWN_TIME = 15f;
    private const float MAX_RESPAWN_TIME = 20f;

    void Start()
    {
        // Ensure no power-ups are instantiated at the start
        DestroyAllActivePowerUps();

        // Ensure the arrays are not empty
        if (powerUpPrefabs.Length == 0)
        {
            Debug.LogError("No power-up prefabs assigned in PowerUpSpawner.");
            return;
        }
        
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned in PowerUpSpawner.");
            return;
        }

        // Start the coroutine to manage power-up spawning
        StartCoroutine(ManagePowerUps());
    }

    // Function to destroy all power-ups present at the start of the game
    private void DestroyAllActivePowerUps()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Remove any existing power-ups that may be in the scene at the start
            foreach (Transform child in spawnPoint)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private IEnumerator ManagePowerUps()
    {
        while (true)
        {
            // Ensure there are no more than 2 power-ups on the screen
            if (activePowerUps.Count < MAX_ACTIVE_POWERUPS)
            {
                // Spawn a new power-up
                SpawnPowerUp();
            }

            // Wait for a random interval before checking again
            yield return new WaitForSeconds(Random.Range(MIN_RESPAWN_TIME, MAX_RESPAWN_TIME));
        }
    }

    // Function to spawn a power-up
    private void SpawnPowerUp()
    {
        if (powerUpPrefabs.Length > 0 && spawnPoints.Length > 0)
        {
            int index = Random.Range(0, powerUpPrefabs.Length); // Randomly select a power-up prefab
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Randomly select a spawn point
            GameObject newPowerUp = Instantiate(powerUpPrefabs[index], spawnPoint.position, Quaternion.identity);
            newPowerUp.SetActive(true);

            // Set the new power-up as a child of the spawn point (if needed)
            newPowerUp.transform.SetParent(spawnPoint);

            // Add the new power-up to the list of active power-ups
            activePowerUps.Add(newPowerUp);

            // Start the power-up expiration timer
            StartCoroutine(ExpirePowerUp(newPowerUp, spawnPoint));
        }
    }

    // Coroutine to handle power-up expiry and regeneration
    private IEnumerator ExpirePowerUp(GameObject powerUp, Transform spawnPoint)
    {
        // Wait for the power-up's active time to expire (15-20 seconds)
        yield return new WaitForSeconds(Random.Range(15f, 20f));

        // Ensure the power-up is not already destroyed
        if (powerUp != null)
        {
            Destroy(powerUp);

            // Remove it from the active list, making sure it still exists
            activePowerUps.Remove(powerUp);
        }

        // Respawn the power-up in the same spot
        SpawnPowerUp();
    }
}
