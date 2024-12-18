using UnityEngine;
using System.Collections; // For Coroutine

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Array of power-up prefabs to spawn
    public Transform[] spawnPoints;    // Predefined spawn points for power-ups
    private GameObject[] currentPowerUps;

    private void Start()
    {
        currentPowerUps = new GameObject[spawnPoints.Length];
        RespawnAllPowerUps();
    }

    private void RespawnAllPowerUps()
    {
        foreach (Transform point in spawnPoints)
        {
            StartCoroutine(RespawnPowerUpAt(point));
        }
    }

    // Coroutine to handle power-up respawn at specific points
    private IEnumerator RespawnPowerUpAt(Transform spawnPoint)
    {
        float respawnTime = Random.Range(1f, 45f); // Random interval between 1 and 45 seconds
        yield return new WaitForSeconds(respawnTime);

        int index = Random.Range(0, powerUpPrefabs.Length);
        GameObject newPowerUp = Instantiate(powerUpPrefabs[index], spawnPoint.position, Quaternion.identity);
        newPowerUp.transform.SetParent(spawnPoint);

        // Destroy any previous power-ups
        if (spawnPoint.childCount > 1)
        {
            Destroy(spawnPoint.GetChild(0).gameObject);
        }
    }
}
