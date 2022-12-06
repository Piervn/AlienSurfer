using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    public float gravityFactor = 1f;
    public float laneOffset = 5f;
    public float environmentSpeed = 1f;
    public float gameOverDelay = 0.4f;

    [HideInInspector]
    public int score = 0;

    const float gravity = 9.81f;

    GameObject player;
    Animator gameOverPopup;
    Vector3 defaultGravity;
    bool gameOver = false;
    
    void Awake() {
        player = GameObject.Find("Player");
        gameOverPopup = GameObject.Find("GameOverPopup").GetComponent<Animator>();
    }

    void Start() {
        EventManager.OnCoinCollect += () => {
            scoreText.text = (++score).ToString();
        };
        EventManager.OnGameOver += () => {
            StartCoroutine(GameOver(gameOverDelay));
        };
        GameObject.FindGameObjectWithTag("LeftTrack").transform.position = Vector3.left * laneOffset;
        GameObject.FindGameObjectWithTag("RightTrack").transform.position = Vector3.right * laneOffset;
    }

    void Update() {
        Physics.gravity = Vector3.down * gravity * gravityFactor;
        if (!gameOver && player.transform.position.z < -10) {
            EventManager.GameOver();
        }
    }

    IEnumerator GameOver(float delay = 0f) {
        yield return new WaitForSeconds(delay);
        gameOver = true;
        gameOverPopup.SetTrigger("GameOver");
        environmentSpeed = 0f;
    }
}
