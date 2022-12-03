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
    public State state = State.Run;
    public float jumpVelocity;

    GameManager gm;
    Lane lane = Lane.Middle;
    Rigidbody rb;
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
        coll = GetComponent<BoxCollider>();
        collCenter = coll.center;
        collSize = coll.size;
    }

    void Update() {
        Land();
    }

    public bool IsGrounded() {
        bool isGrounded = Physics.BoxCast(coll.bounds.center, coll.bounds.extents / 2f, Vector3.down, 
                                          Quaternion.identity, distToGround, groundLayer);

        return isGrounded;
    }

    public void Jump() {
        if (state == State.Run && IsGrounded()) {
            rb.velocity = Vector3.up * jumpVelocity;
            state = State.Jump;
        }
    }

    public void Land() {
        if (state == State.Jump && IsGrounded() && IsFalling) {
            state = State.Run;
        }
    }

    public void MoveRight() {
        if (state == State.Run && lane != Lane.Right) {
            transform.position += Vector3.right * gm.laneOffset;
            lane = (lane == Lane.Middle) ? Lane.Right : Lane.Middle;
        }
    }

    public void MoveLeft() {
        if (state == State.Run && lane != Lane.Left) {
            transform.position += Vector3.left * gm.laneOffset;
            lane = (lane == Lane.Middle) ? Lane.Left : Lane.Middle;
        }
    }

    public void Slide() {
        if (state == State.Run) {
            coll.size = new Vector3(1f, 0.5f, 1f);
            coll.center = new Vector3(0f, 0.25f, 0f);
            state = State.Slide;
        }
    }

    public void EndSliding() {
        coll.size = collSize;
        coll.center = collCenter;
    }

}

