using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
	GameManager gm;
    float length;

    void Awake() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        length = GetComponentInChildren<MeshRenderer>().bounds.size.z;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.z < -length) {
            transform.position += Vector3.forward * length;
        }
        transform.position += Vector3.back * gm.environmentSpeed * Time.deltaTime;
    }
}
