using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
	public GameManager gm;

    void Start() {
        foreach (Transform child in transform) {
            Rigidbody objRb = child.gameObject.AddComponent<Rigidbody>();
            objRb.useGravity = false;
            objRb.isKinematic = true;
            Obstacle obst = child.gameObject.AddComponent<Obstacle>();
            obst.gm = this.gm;
            if (child.gameObject.name == "Torpedo") {
                obst.velocityFactor = gm.torpedoSpeed;
            }
            else if (child.gameObject.name == "Locomotive") {
                obst.velocityFactor = gm.locomotiveSpeed;
            }
        }
    }
    
}
