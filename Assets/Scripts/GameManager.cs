using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gravityFactor = 1f;
    public float laneOffset = 3f;
    public float environmentSpeed = 1f;
    public float obstacleSpawnRate = 1f;
    public float coinsRowSpawnRate = 1f;
    public int coinsInRow = 5;
    public float coinsInRowSpawnRate = 1f;
    public int score = 0;

    Vector3 defaultGravity;
    
    void Awake() {
        defaultGravity = Physics.gravity;
    }

    void Update() {
        Physics.gravity = defaultGravity * gravityFactor;
        Debug.Log(score);
    }
}
