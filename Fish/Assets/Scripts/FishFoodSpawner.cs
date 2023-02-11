using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFoodSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnRegion = new Vector2(38.4f, 21.6f);
    [SerializeField]
    private int maxSpawnedObjects = 30;
    [SerializeField]
    private int spawnTries = 3;
    [SerializeField]
    private GameObject spawnPrefab;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position, spawnRegion);
    }

    private Vector3 getRandomPointInRagion() {
        Vector3 spawnPos = new Vector2(Random.Range(-spawnRegion.x/2f, spawnRegion.x/2f), Random.Range(-spawnRegion.y/2f, spawnRegion.y/2f));
        spawnPos += new Vector3(transform.position.x, transform.position.y);

        return spawnPos;
    }

    private void Update() {
        for (int i = 0; i < maxSpawnedObjects - spawnedObjects.Count; i++) {
            Vector3 worldPosToSpawn = Vector3.zero;
            bool isValidSpawn = true;
            for(int j = 0; j < spawnTries; j++) {
                worldPosToSpawn = getRandomPointInRagion();
                isValidSpawn = !Physics2D.CircleCast(worldPosToSpawn, 0.5f, Vector3.up, 0);

                if (isValidSpawn)
                    break;
            }

            if(isValidSpawn) {
                GameObject spawnObj = Instantiate(spawnPrefab, worldPosToSpawn, Quaternion.Euler(0, 0, 0));
                spawnedObjects.Add(spawnObj);
            }
        }
    }
}
