using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    public bool IsGameOver { get; set; } = false;
    public float gravityFactor = 1f;
    public float environmentSpeed = 1f;
    public float torpedoSpeed = 2f;
    public float locomotiveSpeed = 4f;
    public float gameOverDelay = 0.4f;

    [HideInInspector] public int score = 0;
    [HideInInspector] public float laneOffset = 4f;

    const float gravity = 9.81f;

    GameObject player;
    Animator gameOverPopup;
    Vector3 defaultGravity;
    
    void Awake() {
        player = GameObject.Find("Player");
        gameOverPopup = GameObject.Find("GameOverPopup").GetComponent<Animator>();
    }

    void Start() {
        
        EventManager.OnGameOver += () => {
            StartCoroutine(GameOver(gameOverDelay));
        };
        GameObject.FindGameObjectWithTag("LeftTrack").transform.position = Vector3.left * laneOffset;
        GameObject.FindGameObjectWithTag("RightTrack").transform.position = Vector3.right * laneOffset;
    }

    void Update() {
        Physics.gravity = Vector3.down * gravity * gravityFactor;
    }

    IEnumerator GameOver(float delay = 0f) {
        yield return new WaitForSeconds(delay);
        IsGameOver = true;
        gameOverPopup.SetTrigger("GameOver");
        environmentSpeed = 0f;
    }
}
