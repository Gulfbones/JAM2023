using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class FishSpawnPoint : MonoBehaviour {

    public float SpawnRadius { get { return circleCollider.radius; } }

    [SerializeField]
    private int maxFishCount = 5;
    [SerializeField]
    private float despawnTimer = 5.0f;
    [SerializeField]
    private float spawnTimer = 5.0f;
    [SerializeField]
    private List<GameObject> fishesToSpawn = new List<GameObject>();

    private CircleCollider2D circleCollider;
    private static int maxFishCountGlobal = 500;
    public static int fishCountGlobal = 0;
    private static GameObject player;

    public bool doFishSpawning = false;

    private void Awake() {
        circleCollider = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<PlayerInput>().gameObject;
    }

    private void OnEnable() {
        StartCoroutine(SpawnFish());
        StartCoroutine(DespawnFish());
    }

    private IEnumerator DespawnFish() {
        yield return new WaitForSeconds(despawnTimer);

        if(!doFishSpawning && transform.childCount > 0 && EvaluatePlayerPosition()) {
            GameObject fish = transform.GetChild(0).gameObject;

            Destroy(fish);
            fishCountGlobal -= 1;
        }
        StartCoroutine(DespawnFish());
    }
    private IEnumerator SpawnFish() {
        yield return new WaitForSeconds(spawnTimer);

        if (transform.childCount < maxFishCount && fishCountGlobal < maxFishCountGlobal && doFishSpawning && EvaluatePlayerPosition()) {
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

    public bool EvaluatePlayerPosition() {
        Vector3 playerPos = player.transform.position;
        float distX = Mathf.Abs(playerPos.x - transform.position.x);
        float distY = Mathf.Abs(playerPos.y - transform.position.y);

        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * (1920f / 1080f);

        return (distX > (SpawnRadius + camWidth)) || (distY > (SpawnRadius + camHeight));
    }
}
