using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingSpawner : MonoBehaviour {
    public GameObject homingPrefab;  // Assign your envelope prefab
    public Transform player;         // Assign player
    public float spawnRate = 2f;     // Time between spawns
    public float spawnDistance = 10f; // Distance behind player
    public float spawnRange = 5f;    // Spread of spawn positions

    void Start() {
        StartCoroutine(SpawnHomingMissiles());
    }

    IEnumerator SpawnHomingMissiles() {
        while (true) {
            yield return new WaitForSeconds(spawnRate);

            if (player != null) {
                SpawnMissile();
            }
        }
    }

    void SpawnMissile() {
    // Determine the spawn position behind the player
    float xOffset = Random.Range(-spawnRange, spawnRange);
    float spawnX = player.position.x - spawnDistance; // Spawn behind
    float spawnY = player.position.y + xOffset; // Random vertical offset

    Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);

    GameObject missile = Instantiate(homingPrefab, spawnPos, Quaternion.identity);
    Homing homingScript = missile.GetComponent<Homing>();

    if (homingScript != null) {
        homingScript.target = player;
    }
}

}
