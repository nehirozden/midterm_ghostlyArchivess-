using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothSpawner : MonoBehaviour
{
    public GameObject mothPrefab; // Reference to the Moth prefab

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DuplicateMoth", 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DuplicateMoth()
    {
        if (mothPrefab != null)
        {
            // Random position
            float randomY = Random.Range(1f, 11f);

            Vector2 randomSpawnPosition = new Vector2(10f, randomY);

            // Instantiate the new moth at the specified spawn position
            Instantiate(mothPrefab, randomSpawnPosition, Quaternion.identity);

            Debug.Log("Moth duplicated and spawned at the top-right!");
        }
    }
}
