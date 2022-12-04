using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    GameManager gm;
    PlayerMovement pm;
	Animator anim;

    const float animJumpSpeedFactor = 3.8f;
    const float animRunSpeedFactor = 6f;
    const float animSideSpeedFactor = 10f;


    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        pm = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("RunSpeed", gm.environmentSpeed / animRunSpeedFactor);
    }

    public void JumpAnimation() {
        if (pm.state == State.Jump && pm.IsGrounded()) {
            anim.SetFloat("JumpSpeed", gm.gravityFactor * animJumpSpeedFactor / pm.jumpVelocity);
            anim.Play("Jumping");
        }
    }

    public void MoveLeftAnimation() {
        if (pm.state == State.Run) {
            anim.SetFloat("SideSpeed", pm.sideMovemnentVelocity / animSideSpeedFactor);
            anim.Play("RunningLeft");
        }
    }

    public void MoveRightAnimation() {
        if (pm.state == State.Run) {
            anim.SetFloat("SideSpeed", pm.sideMovemnentVelocity / animSideSpeedFactor);
            anim.Play("RunningRight");
        }
    }

    public  void SlideAnimation() {
        if (pm.state == State.Slide && pm.lane != Lane.Left) {
            anim.Play("Sliding");
        }
    }
}
