using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    GameManager gm;
    TimeBar tb;
    Rigidbody rb;
    PlayerMovement pm;
    PlayerAnimations pa;
    float timeToLive = 5f;
    float height = 14f;
    float upVelocity = 10f;
    float speed = 4f;
	
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        tb = GameObject.Find("JetpackTimeBar").GetComponent<TimeBar>();
        tb.SetMaxTime(timeToLive);
        rb = GetComponent<Rigidbody>();
        pa = GetComponent<PlayerAnimations>();
        pm = GetComponent<PlayerMovement>();
        StartFlying();
    }

    void Update() {
        timeToLive -= Time.deltaTime;
        tb.SetTime(timeToLive);
        if (timeToLive <= 0) {
            Destroy(this);
        }
    }

    void StartFlying() {
        foreach (Transform child in gm.spawner.transform) {
            Destroy(child.gameObject);
        }
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        gm.environmentSpeed *= speed;
        StartCoroutine(Fly());
        pm.state = State.Fly;
        pa.FlyAnimation();
    }

    void EndFlying() {
        gm.environmentSpeed /= speed;
        rb.useGravity = true;
    }

    IEnumerator Fly() {
        while (transform.position.y < height) {
            transform.position += Vector3.up * upVelocity * Time.deltaTime;
            yield return null;
        }
    }

    void OnDestroy() {
        EndFlying();
    }
}
