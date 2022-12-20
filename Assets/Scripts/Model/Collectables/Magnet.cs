using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    GameManager gm;
    TimeBar tb;
    float radius = 10f;
    float forceFactor = 1.4f;
    float timeToLive = 5f;
    Vector3 offset = new Vector3(0, 1, 0);
    int runningCoroutines = 0;

    void Start() {
        gm = FindObjectOfType<GameManager>();
        tb = GameObject.Find("MagnetTimeBar").GetComponent<TimeBar>();
        tb.SetMaxTime(timeToLive);
    }

	void Update() {
        if (timeToLive <= 0) return;
        DetectCoins();
        timeToLive -= Time.deltaTime;
        tb.SetTime(timeToLive);
        if (timeToLive <= 0) {
            StartCoroutine(DestroyMagnet());
        }
    }

    void DetectCoins() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders) {
            if (collider.CompareTag("Coin")) {
                StartCoroutine(Attract(collider.transform));
                collider.tag = "Untagged";
            }
        }
    }

    IEnumerator Attract(Transform obj) {
        runningCoroutines++;
        while (obj != null){
            obj.position = Vector3.MoveTowards(obj.position, transform.position + offset, forceFactor * gm.environmentSpeed * Time.deltaTime);
            yield return null;
        }
        runningCoroutines--;
    }


    IEnumerator DestroyMagnet() {
        while (runningCoroutines > 0) {
            yield return null;
        }
        Destroy(this);
    }
}