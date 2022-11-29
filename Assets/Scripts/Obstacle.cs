using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameManager gm;

    void Update()
    {
        if (transform.position.z < -10) {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.back * gm.environmentSpeed * Time.deltaTime);
    }
}
