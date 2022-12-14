using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoots : MonoBehaviour
{
    GameManager gm;
    TimeBar tb;
    PlayerMovement pm;
    float extraJumpForce = 8f;
    float timeToLive = 15f;

    void Start()
    {
        tb = GameObject.Find("JumpBootsTimeBar").GetComponent<TimeBar>();
        tb.SetMaxTime(timeToLive);
        pm = GetComponent<PlayerMovement>();
        IncreaseJumpForce();
    }

    void Update()
    {
        timeToLive -= Time.deltaTime;
        tb.SetTime(timeToLive);
        if (timeToLive <= 0) {
            Destroy(this);
        }
    }

    void IncreaseJumpForce() {
        pm.jumpVelocity += extraJumpForce;
    }

    void DecreaseJumpForce() {
        pm.jumpVelocity -= extraJumpForce;
    }

    void OnDestroy() {
        DecreaseJumpForce();
    }
}
