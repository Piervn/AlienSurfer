using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    GameManager gm;
    float radius = 10f;
    float forceFactor = 1.5f;
    Vector3 offset = new Vector3(0, 1, 0);

    void Start() {
        gm = FindObjectOfType<GameManager>();
    }

	void Update() {
        DetectCoins();
    }

    void DetectCoins() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders) {
            if (collider.CompareTag("Coin")) {
                StartCoroutine(Attract(collider.transform));
            }
        }
    }

    IEnumerator Attract(Transform obj) {
        obj.position = Vector3.MoveTowards(obj.position, transform.position + offset, forceFactor * gm.environmentSpeed * Time.deltaTime);
        yield return null;
    }
}