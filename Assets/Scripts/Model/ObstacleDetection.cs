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
    float raycastDistance = 6f;
    
	
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
    }

    void Update()
    {
        rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionZ;
        if (DetectObstacle(Vector3.forward) ||
            (pm.lane != Lane.Left && DetectObstacle(Vector3.left)) ||
            (pm.lane != Lane.Right && DetectObstacle(Vector3.right)))
        {
            if (hit.collider.CompareTag("Obstacle")) {
                rb.constraints = rb.constraints & ~RigidbodyConstraints.FreezePositionZ;
            }
        }
    }

    bool DetectObstacle(Vector3 dir) {
        bool result = Physics.BoxCast(coll.bounds.center, coll.bounds.extents / 2f, dir, out hit, Quaternion.identity, raycastDistance);
        Debug.DrawRay(transform.position + raycastOffset, transform.TransformDirection(dir) * raycastDistance, Color.white);
        if (result) {
            Debug.DrawRay(transform.position + raycastOffset, transform.TransformDirection(dir) * hit.distance, Color.yellow);
        }
        return result;
    }
}
