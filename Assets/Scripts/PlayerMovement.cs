using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private LayerMask groundLayer;
    public float jumpForce;
    private Rigidbody rb;
    private Animator anim;
    private BoxCollider coll;

    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            rb.velocity = Vector3.up * jumpForce;
            anim.SetFloat("SpeedFactor", 4f / jumpForce);
            anim.Play("Jumping");
        }
        //Debug.DrawRay(coll.bounds.center, Vector3.down);
    }

    bool IsGrounded() {
        float distance = 0.7f;
        bool isGrounded = Physics.BoxCast(coll.bounds.center,
                                          coll.bounds.extents / 2,
                                          Vector3.down,
                                          Quaternion.identity,
                                          distance,
                                          groundLayer);

        return isGrounded;
    }
}

