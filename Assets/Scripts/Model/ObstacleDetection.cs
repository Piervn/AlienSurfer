using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour
{
    PlayerMovement pm;
    Rigidbody rb;
    BoxCollider coll;
    Vector3 raycastOffset = new Vector3(0, 1.2f, 0);
    RaycastHit hit;
    float raycastDistance = 2f;
    
	
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
    }

    void LateUpdate()
    {
        pm.ObjectDetected = false;
        if (DetectObstacle(Vector3.forward) ||
            (pm.lane != Lane.Left && DetectObstacle(Vector3.left)) ||
            (pm.lane != Lane.Right && DetectObstacle(Vector3.right)))
        {
            if (hit.collider.CompareTag("Obstacle")) {
                pm.ObjectDetected = true;
            }
        }
    }

    bool DetectObstacle(Vector3 dir) {
        bool result = Physics.BoxCast(coll.bounds.center, coll.bounds.extents / 2f, dir, out hit, Quaternion.identity, raycastDistance);
        Debug.DrawRay(transform.position + raycastOffset, dir * raycastDistance, Color.white);
        if (result) {
            Debug.DrawRay(transform.position + raycastOffset, dir * hit.distance, Color.yellow);
        }
        return result;
    }
}
