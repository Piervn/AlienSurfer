using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    
    GameManager gameManager;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SpawnObstacles() {
        while (true) {
            yield return new WaitForSeconds(gameManager.spawnRate);
            Vector3 lane = Random.Range(-1, 2) * Vector3.right * gameManager.laneOffset;
            GameObject obst = Instantiate(obstaclePrefab, transform);
            obst.transform.position += lane;
            obst.GetComponent<Obstacle>().gm = gameManager;
        }
    }
}
