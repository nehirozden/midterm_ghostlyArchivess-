using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothSpawner : MonoBehaviour
{
    public GameObject bigMothPrefab;
    public GameObject smallMothPrefab;

    // Initial spawn interval
    public float timeBetweenSpawnBig = 7f;
    public float timeBetweenSpawnSmall = 10f;

    // Initial spawning time
    public float bigSpawnStartTime = 0f;
    public float smallSpawnStartTime = 5f;

    // How much it gets faster
    public float bigSpawnDecrement = 0.5f;
    public float smallSpawnDecrement = 0.5f;

    // How often it gets faster
    public float bigSpawnDecrementTime = 21f;
    public float smallSpawnDecrementTime = 21f;

    // Fastest time to spawn
    public float spawnCap = 3f;


    // Start is called before the first frame update
    void Start()
    {
        // Start the spawners
        InvokeRepeating("DuplicateMothBig", bigSpawnStartTime, timeBetweenSpawnBig);
        InvokeRepeating("DuplicateMothSmall", smallSpawnStartTime, timeBetweenSpawnSmall);

        InvokeRepeating("changeBig", bigSpawnDecrementTime, bigSpawnDecrementTime);
        InvokeRepeating("changeSmall", smallSpawnDecrementTime, smallSpawnDecrementTime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Make big moth spawning faster
    void changeBig() {
        if (timeBetweenSpawnBig > spawnCap) {
            CancelInvoke("DuplicateMothBig");
            timeBetweenSpawnBig -= bigSpawnDecrement;
            InvokeRepeating("DuplicateMothBig", 0f, timeBetweenSpawnBig);
            Debug.Log("Faster big");
        }
    }

    // Make small moth spawning faster
    void changeSmall() {
        if (timeBetweenSpawnSmall > spawnCap) {
            CancelInvoke("DuplicateMothSmall");
            timeBetweenSpawnSmall -= smallSpawnDecrement;
            InvokeRepeating("DuplicateMothSmall", 0f, timeBetweenSpawnSmall);
            Debug.Log("Faster small");
        }
    }


    public void DuplicateMothBig()
    {
        if (bigMothPrefab != null)
        {
            // Random position
            float randomY = Random.Range(1f, 11f);

            Vector2 randomSpawnPosition = new Vector2(10f, randomY);

            // Instantiate the new moth at the specified spawn position
            Instantiate(bigMothPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }

    public void DuplicateMothSmall()
    {
        if (smallMothPrefab != null)
        {
            // Random position
            float randomY = Random.Range(1f, 11f);

            Vector2 randomSpawnPosition = new Vector2(10f, randomY);

            // Instantiate the new moth at the specified spawn position
            Instantiate(smallMothPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }
}
