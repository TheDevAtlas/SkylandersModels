using UnityEngine;

public class randomspawner : MonoBehaviour
{
    public GameObject[] things; // Array of objects to spawn
    public float spawnInterval = 2.0f; // Interval between spawns
    public Transform spawnLocation; // Location to spawn objects
    public float minRotationalVelocity = -10f; // Minimum rotational velocity
    public float maxRotationalVelocity = 10f;  // Maximum rotational velocity

    void Start()
    {
        // Start the coroutine to spawn objects
        StartCoroutine(SpawnRandomObject());
    }

    // Coroutine to spawn objects at regular intervals
    private System.Collections.IEnumerator SpawnRandomObject()
    {
        while (true) // Infinite loop
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(spawnInterval);

            // Choose a random object from the array
            GameObject randomThing = things[Random.Range(0, things.Length)];

            // Randomize position, rotation, and spawn object
            Quaternion randomRotation = Random.rotation; // Generate a random rotation
            GameObject spawnedObject = Instantiate(randomThing, spawnLocation.position, randomRotation);

            // Add a Rigidbody component if not already present
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = spawnedObject.AddComponent<Rigidbody>();
                spawnedObject.AddComponent<SphereCollider>();
            }

            // Set a random angular velocity
            Vector3 randomAngularVelocity = new Vector3(
                Random.Range(minRotationalVelocity, maxRotationalVelocity),
                Random.Range(minRotationalVelocity, maxRotationalVelocity),
                Random.Range(minRotationalVelocity, maxRotationalVelocity)
            );

            rb.angularVelocity = randomAngularVelocity; // Apply random rotational velocity
        }
    }
}
