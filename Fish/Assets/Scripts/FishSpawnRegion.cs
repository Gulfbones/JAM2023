using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FishSpawnRegion : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPointsHolder;

    private bool isPlayerInside;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<PlayerInput>() != null) {
            isPlayerInside = true;

            for(int i = 0; i < spawnPointsHolder.childCount; i++) {
                spawnPointsHolder.GetChild(i).GetComponent<FishSpawnPoint>().doFishSpawning = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<PlayerInput>() != null) {
            isPlayerInside = false;

            for (int i = 0; i < spawnPointsHolder.childCount; i++) {
                spawnPointsHolder.GetChild(i).GetComponent<FishSpawnPoint>().doFishSpawning = true;
            }
        }
    }


}
