using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FishSpawnPoint : MonoBehaviour {
    [SerializeField]
    private int maxFishCount = 5;
    [SerializeField]
    private float despawnTimer = 5.0f;

    private List<GameObject> currentFish = new List<GameObject>();

    private static int maxFishCountGlobal = 30;
    private static int currentFishCount;

    private bool isPlayerInside;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<PlayerInput>() != null) {
            isPlayerInside = true;


        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<PlayerInput>() != null) {
            isPlayerInside = false;
        }
    }

    private IEnumerator DespawnFish() {
        yield return new WaitForSeconds(despawnTimer);

        if(isPlayerInside) {

        }
    }
}
