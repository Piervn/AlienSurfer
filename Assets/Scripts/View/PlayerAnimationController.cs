using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    GameManager gm;
    PlayerMovement pm;
	Animator anim;

    const float animSpeedFactor = 3.8f;


    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        pm = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("RunSpeed", gm.environmentSpeed / 6f);
    }

    public void JumpAnimation() {
        if (pm.state == State.Jump && pm.IsGrounded()) {
            anim.SetFloat("JumpSpeed", gm.gravityFactor * animSpeedFactor / pm.jumpVelocity);
            anim.SetTrigger("Jump");
            anim.Play("Jumping");
        }
    }

    public  void SlideAnimation() {
        if (pm.state == State.Slide) {
            anim.SetBool("IsSliding", true);
            anim.Play("Sliding");
        }
    }

    public void EndSlideAnimation() {
        pm.state = State.Run;
        anim.SetBool("IsSliding", false);
    }
}
