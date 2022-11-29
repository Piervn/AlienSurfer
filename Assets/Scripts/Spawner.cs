using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject coinPrefab;
    public List<GameObject> obstacles = new List<GameObject>();

    GameManager gameManager;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start() {
        //StartCoroutine(SpawnObstacles());
        StartCoroutine(SpawnCoins());
        //SpawnCoinsRow(0);
    }

    void Update() {

    }

    IEnumerator SpawnObstacles() {
        while (true) {
            yield return new WaitForSeconds(gameManager.obstacleSpawnRate);
            Vector3 lane = Random.Range(-1, 2) * Vector3.right * gameManager.laneOffset;
            GameObject obj = Instantiate(obstacles[Random.Range(0, obstacles.Count)], transform);
            obj.transform.position += lane;
            Rigidbody objRb = obj.AddComponent<Rigidbody>();
            objRb.useGravity = false;

            Obstacle obst = obj.AddComponent<Obstacle>();
            obst.gm = gameManager;
        }
    }

    IEnumerator SpawnCoins() {
        while (true) {
            yield return new WaitForSeconds(gameManager.coinsRowSpawnRate);
            yield return StartCoroutine(SpawnCoinsRow(Random.Range(-1, 2)));
        }
    }

    IEnumerator SpawnCoinsRow(int laneVal) {
        var wait = new WaitForSeconds(gameManager.coinsInRowSpawnRate);
        Vector3 lane = Vector3.right * laneVal * gameManager.laneOffset;
        for (int i = 0; i < gameManager.coinsInRow; i++) {
            yield return new WaitForSeconds(gameManager.coinsInRowSpawnRate);
            GameObject obj = Instantiate(coinPrefab, transform);
            obj.transform.position += lane;

            Collectable coin = obj.AddComponent<Collectable>();
            coin.gm = gameManager;
        }
    }
}
