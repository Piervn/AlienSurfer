using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum State {
    Run,
    Jump,
    Slide,
    Fall,
}

public enum Lane {
    Left,
    Middle,
    Right,
}

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private LayerMask groundLayer;
    public float jumpVelocity;

    GameManager gm;
    State state = State.Run;
    Lane lane = Lane.Middle;
    
    Rigidbody rb;
    
    Animator anim;
    const float animSpeedFactor = 3.8f;
    
    BoxCollider coll;
    Vector3 collCenter;
    Vector3 collSize;
    const float distToGround = 0.6f;

    bool IsFalling {
        get {
            return rb.velocity.y < 0f;
        }
    }

    void Awake() {
        gm = FindObjectOfType<GameManager>();
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider>();
        collCenter = coll.center;
        collSize = coll.size;
    }

    void Update() {
        //Debug.Log(state);
        if (state == State.Run) {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
                rb.velocity = Vector3.up * jumpVelocity;
                state = State.Jump;
                anim.SetFloat("JumpSpeed", gm.gravityFactor * animSpeedFactor / jumpVelocity);
                anim.SetBool("IsJumping", true);
                anim.Play("Jumping");
            }
            if (lane != Lane.Left && Input.GetKeyDown(KeyCode.A)) {
                transform.position += Vector3.left * gm.laneOffset;
                lane = (lane == Lane.Middle) ? Lane.Left : Lane.Middle;
            }
            if (lane != Lane.Right && Input.GetKeyDown(KeyCode.D)) {
                transform.position += Vector3.right * gm.laneOffset;
                lane = (lane == Lane.Middle) ? Lane.Right : Lane.Middle;
            }
        } 
        if (state == State.Run && Input.GetKeyDown(KeyCode.S)) {
            coll.size = new Vector3(1f, 0.5f, 1f);
            coll.center = new Vector3(0f, 0.25f, 0f);
            state = State.Slide;
            anim.SetBool("IsSliding", true);
            anim.Play("Sliding");
        }
        if (state == State.Jump && IsFalling && IsGrounded() ) {
            state = State.Run;
            anim.SetBool("IsJumping", false);
        }
        anim.SetFloat("RunSpeed", gm.environmentSpeed / 6f);
    }

    bool IsGrounded() {
        bool isGrounded = Physics.BoxCast(coll.bounds.center, coll.bounds.extents / 2f, Vector3.down, 
                                          Quaternion.identity, distToGround, groundLayer);

        return isGrounded;
    }

    public void EndSliding() {
        coll.size = collSize;
        coll.center = collCenter;
        state = State.Run;
        anim.SetBool("IsSliding", false);
    }

}

