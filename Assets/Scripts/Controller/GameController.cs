using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    PlayerMovement player;
    PlayerAnimationController playerAnim;

    void Awake() {
        player = FindObjectOfType<PlayerMovement>();
        playerAnim = FindObjectOfType<PlayerAnimationController>();
    }

    void Start() {
        EventManager.OnSpacePress += () => {
            player.Jump();
            playerAnim.JumpAnimation();
        };

        EventManager.OnLeftPress += () => {
            player.MoveLeft();
        };

        EventManager.OnRightPress += () => {
            player.MoveRight();
        };

        EventManager.OnDownPress += () => {
            player.Slide();
            playerAnim.SlideAnimation();
        };

    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) {
            EventManager.SpacePress();
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            EventManager.LeftPress();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            EventManager.RightPress();
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            EventManager.DownPress();
        }
    }

}
