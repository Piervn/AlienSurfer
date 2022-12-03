using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    public Animator envAnim;
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

    void Start() {
        EventManager.OnCoinCollect += () => {
            //score++;
            scoreText.text = (++score).ToString();
        };
        GameObject.FindGameObjectWithTag("LeftTrack").transform.position = Vector3.left * laneOffset;
        GameObject.FindGameObjectWithTag("RightTrack").transform.position = Vector3.right * laneOffset;
        envAnim = GameObject.FindGameObjectWithTag("Environment").GetComponent<Animator>();
        envAnim.SetFloat("EnvSpeed", environmentSpeed / 10f);
    }

    void Update() {
        Physics.gravity = defaultGravity * gravityFactor;
    }
}
