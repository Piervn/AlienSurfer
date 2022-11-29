using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gravityFactor = 1f;
    public float laneOffset = 3f;
    public float environmentSpeed = 1f;
    public float spawnRate = 1f;

    Vector3 defaultGravity;
    
    void Awake() {
        defaultGravity = Physics.gravity;
    }

    void Update() {
        Physics.gravity = defaultGravity * gravityFactor;
    }
}
