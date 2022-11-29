using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType {
    Coin,
    Magnet,
    Jetpack,
    JumpBoots,
}

public class Collectable : MonoBehaviour
{
    public GameManager gm;
    

    void Update()
    {
        if (transform.position.z < -10) {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.back * gm.environmentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            gm.score += 1;
            Destroy(gameObject);
        }
    }
}
