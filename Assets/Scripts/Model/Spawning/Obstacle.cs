using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameManager gm;
    public float velocityFactor = 1f;

    void Update()
    {
        if (transform.position.z < -100) {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.back * gm.environmentSpeed * velocityFactor * Time.deltaTime);
    }
}
