using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject coinPrefab;
    public List<GameObject> obstacles = new List<GameObject>();
    public List<GameObject> scenarios = new List<GameObject>();
    public List<GameObject> bonuses = new List<GameObject>();
    public int coinsInRow = 5;
    public float coinsRowSpawnRate = 1f;
    public float coinsInRowSpawnRate = 1f;
    public float obstacleSpawnRate = 1f;
    public float scenarioSpawnRate = 1f;
    public float obstacleVelocityFactor = 1f;

    float raycastSourceOffset = 20f;

    GameManager gameManager;
    AudioManager audioManager;
    
    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start() {
        StartCoroutine(SpawnCoins());
        StartCoroutine(SpawnBonuses());
        StartCoroutine(SpawnScenarios());
        //StartCoroutine(RandomSpawn());
    }
    void Update() {
        GetSpawnPosition(Vector3.zero);
    }


    IEnumerator SpawnCoins() {
        while (true) {
            yield return new WaitForSeconds(coinsRowSpawnRate);
            yield return StartCoroutine(SpawnCoinsRow(Random.Range(-1, 2)));
        }
    }

    IEnumerator SpawnCoinsRow(int laneVal) {
        Vector3 lane = Vector3.right * laneVal * gameManager.laneOffset;
        for (int i = 0; i < coinsInRow; i++) {
            yield return new WaitForSeconds(coinsInRowSpawnRate / gameManager.environmentSpeed);
            GameObject obj = Instantiate(coinPrefab, transform);
            obj.transform.position += lane;

            Collectable coin = obj.AddComponent<Collectable>();
            coin.type = CollectableType.Coin;
            coin.gm = gameManager;
            coin.am = audioManager;
        }
    }

    IEnumerator SpawnBonuses() {
        while (!gameManager.IsGameOver) {
            Vector3 lane = Random.Range(-1, 2) * Vector3.right * gameManager.laneOffset;
            GameObject obj = Instantiate(bonuses[Random.Range(0, bonuses.Count)], GetSpawnPosition(lane), Quaternion.identity, transform);

            Collectable bonus = obj.AddComponent<Collectable>();
            switch (obj.tag) {
                case "Magnet":
                    bonus.type = CollectableType.Magnet;
                    break;
                case "JumpBoots":
                    bonus.type = CollectableType.JumpBoots;
                    break;
                case "Jetpack":
                    bonus.type = CollectableType.Jetpack;
                    break;
            }
            bonus.gm = gameManager;
            bonus.am = audioManager;
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    IEnumerator SpawnScenarios() {
        while (!gameManager.IsGameOver) {
            GameObject obj = Instantiate(scenarios[Random.Range(0, scenarios.Count)], transform);
            float objDepth = obj.transform.GetChild(0).localPosition.z;
            Scenario scenario = obj.AddComponent<Scenario>();
            scenario.gm = gameManager;
            float timeOffset = objDepth / gameManager.environmentSpeed;
            yield return new WaitForSeconds(timeOffset + scenarioSpawnRate);
        }
    }

    IEnumerator RandomSpawn() {
        while (!gameManager.IsGameOver) {
            Vector3 lane = Random.Range(-1, 2) * Vector3.right * gameManager.laneOffset;
            GameObject obj = Instantiate(obstacles[Random.Range(0, obstacles.Count)], transform);
            obj.transform.position += lane;
            Rigidbody objRb = obj.AddComponent<Rigidbody>();
            objRb.useGravity = false;
            objRb.isKinematic = true;

            Obstacle obst = obj.AddComponent<Obstacle>();
            obst.gm = gameManager;
            yield return new WaitForSeconds(obstacleSpawnRate);
        }
    }

    Vector3 GetSpawnPosition(Vector3 lane) {
        RaycastHit hit;
        bool result = Physics.Raycast(transform.position + (Vector3.up * raycastSourceOffset) + lane, Vector3.down, out hit, 100);
        //Debug.DrawRay(transform.position + (Vector3.up * raycastSourceOffset) + lane, Vector3.down * 100, Color.red);
        if (result) {
            //Debug.DrawRay(transform.position + (Vector3.up * raycastSourceOffset) + lane, Vector3.down * hit.distance, Color.yellow);
        }
        return hit.point;
    }
    
}
