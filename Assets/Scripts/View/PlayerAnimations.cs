using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    GameManager gm;
    PlayerMovement pm;
	Animator anim;
    int landVariation = 0;

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
        if (pm.state == State.Run && pm.IsGrounded()) {
            anim.SetFloat("JumpSpeed", gm.gravityFactor * animJumpSpeedFactor / pm.jumpVelocity);
            anim.Play("Jumping");
        }
    }

    public void FallAnimation() {
        anim.SetBool("Landing", false);
        anim.SetBool("Falling", true);
    }

    public void LandAnimation() {
        switch (landVariation) {
            case 0:
                anim.SetBool("Landing", true);
                break;
            case 1:
                anim.SetBool("Rolling", true);
                break;
        }
        anim.SetBool("Falling", false);
    }

    public void MoveSidewaysAnimation(bool direction) {
        if (pm.state == State.Run) {
            anim.SetFloat("SideSpeed", pm.sideMovementVelocity / animSideSpeedFactor);
            if (direction && pm.lane != Lane.Right) {
                anim.Play("RunningRight");
            } else if (!direction && pm.lane != Lane.Left) {
                anim.Play("RunningLeft");
            }
        }
    }

    public  void SlideAnimation() {
        if (pm.state == State.Run) {
            anim.Play("Sliding");
        }
    }

    public void RollDownAnimation() {
        if (pm.state == State.Fall || pm.state == State.Jump) {
            anim.SetBool("Landing", false);
            anim.Play("Falling");
            landVariation = 1;
        }
    }

    public void EndRollDownAnimation() {
        anim.SetBool("Rolling", false);
        landVariation = 0;
    }
}
