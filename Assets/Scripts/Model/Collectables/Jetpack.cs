using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    GameManager gm;
    TimeBar tb;
    Rigidbody rb;
    float timeToLive = 4f;
    float height = 10f;
    float upVelocity = 5f;
    float speed = 4f;
	
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        tb = GameObject.Find("JetpackTimeBar").GetComponent<TimeBar>();
        tb.SetMaxTime(timeToLive);
        rb = GetComponent<Rigidbody>();
        StartFlying();
    }

    void Update()
    {
        timeToLive -= Time.deltaTime;
        tb.SetTime(timeToLive);
        if (timeToLive <= 0) {
            EndFlying();
            Destroy(this);
        }
    }

    void StartFlying() {
        rb.useGravity = false;
        gm.environmentSpeed *= speed;
        StartCoroutine(Fly());
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
}
