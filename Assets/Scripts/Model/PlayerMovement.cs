using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum State {
    Run,
    Jump,
    Sideways,
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
    public Lane lane = Lane.Middle;
    public float jumpVelocity;
    public float sideMovemnentVelocity;

    GameManager gm;
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
            StartCoroutine(MoveToLane(lane + 1));
        }
    }


    public void MoveLeft() {
        if (state == State.Run && lane != Lane.Left) {
            StartCoroutine(MoveToLane(lane - 1));
        }
    }

    IEnumerator MoveToLane(Lane targetLane) {
        float targetX = 0f;
        switch (targetLane) {
            case Lane.Left:
                targetX = -gm.laneOffset;
                break;
            case Lane.Middle:
                targetX = 0f;
                break;
            case Lane.Right:
                targetX = gm.laneOffset;
                break;
        }
        state = State.Sideways;
        while (Mathf.Abs(transform.position.x - targetX) > 0.01f) {
            transform.position = Vector3.MoveTowards(transform.position, 
                                                     new Vector3(targetX, transform.position.y, transform.position.z), 
                                                     sideMovemnentVelocity * Time.deltaTime);
            yield return null;
        }
        state = State.Run;
        lane = targetLane;
    }

    public void Slide() {
        if (state == State.Run) {
            coll.size = new Vector3(1f, 0.5f, 1f);
            coll.center = new Vector3(0f, 0.25f, 0f);
            state = State.Slide;
        }
    }

    public void EndSliding() {
        state = State.Run;
        coll.size = collSize;
        coll.center = collCenter;
    }

}

