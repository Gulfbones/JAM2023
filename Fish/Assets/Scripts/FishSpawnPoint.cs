using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class FishSpawnPoint : MonoBehaviour {
    [SerializeField]
    private int maxFishCount = 5;
    [SerializeField]
    private float despawnTimer = 5.0f;
    [SerializeField]
    private float spawnTimer = 5.0f;
    [SerializeField]
    private List<GameObject> fishesToSpawn = new List<GameObject>();

    private CircleCollider2D circleCollider;
    private static int maxFishCountGlobal = 30;
    private static int fishCountGlobal = 0;

    private bool isPlayerInside;

    private void Awake() {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<PlayerInput>() != null) {
            isPlayerInside = true;
            StartCoroutine(SpawnFish());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<PlayerInput>() != null) {
            isPlayerInside = false;
            StartCoroutine(DespawnFish());
        }
    }

    private IEnumerator DespawnFish() {
        yield return new WaitForSeconds(despawnTimer);

        if(!isPlayerInside && transform.childCount > 0) {
            GameObject fish = transform.GetChild(0).gameObject;

            Destroy(fish);
            fishCountGlobal -= 1;
        }
        StartCoroutine(DespawnFish());
    }
    private IEnumerator SpawnFish() {
        yield return new WaitForSeconds(spawnTimer);

        if (transform.childCount < maxFishCount && fishCountGlobal < maxFishCountGlobal && isPlayerInside) {
            GameObject fishToSpawn = fishesToSpawn[Random.Range(0, fishesToSpawn.Count)];

            float theta = Random.Range(0, 2 * Mathf.PI);
            float dist = Random.Range(0, circleCollider.radius);

            Vector3 displacementVector = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * dist;

            Vector3 worldPos = transform.position + displacementVector;

            GameObject spawnedFish = Instantiate(fishToSpawn, worldPos, Quaternion.Euler(0, 0, 0), gameObject.transform);
            fishCountGlobal += 1;

            spawnedFish.GetComponent<FishAI>().parentSpawnPoint = this;
        }
        StartCoroutine(SpawnFish());
    }


}
