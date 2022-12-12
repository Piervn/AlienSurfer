using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    PlayerMovement player;
    PlayerAnimations playerAnim;

    void Awake() {
        player = FindObjectOfType<PlayerMovement>();
        playerAnim = FindObjectOfType<PlayerAnimations>();
    }

    void Start() {
        EventManager.OnSpacePress += () => {
            playerAnim.JumpAnimation();
            player.Jump();
        };

        EventManager.OnLeftPress += () => {
            playerAnim.MoveSidewaysAnimation(false);
            player.MoveLeft();
        };

        EventManager.OnRightPress += () => {
            playerAnim.MoveSidewaysAnimation(true);
            player.MoveRight();
        };

        EventManager.OnDownPress += () => {
            playerAnim.SlideAnimation();
            player.Slide();
            playerAnim.RollDownAnimation();
            player.RollDown();
        };

        EventManager.OnPlayerLands += () => {
            playerAnim.LandAnimation();
            player.Land();
        };

        EventManager.OnPlayerFalls += () => {
            playerAnim.FallAnimation();
            player.Fall();
        };

    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
            EventManager.SpacePress();
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            EventManager.LeftPress();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            EventManager.RightPress();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            EventManager.DownPress();
        }
    }

    void OnDestroy() {
        EventManager.ClearEvents();
    }

}
