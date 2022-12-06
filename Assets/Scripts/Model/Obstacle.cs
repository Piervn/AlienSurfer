using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float velocity = 10f;

    void Update()
    {
        if (transform.position.z < -100) {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.back * velocity * Time.deltaTime);
    }
}
