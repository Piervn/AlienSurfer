using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType {
    Coin,
    Magnet,
    Jetpack,
    JumpBoots,
}

public class Collectable : MonoBehaviour
{
    public GameManager gm;
    public AudioManager am;
    public CollectableType type;
    
    void Update()
    {
        if (transform.position.z < -10) {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.back * gm.environmentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") return;
        switch (type) {
            case CollectableType.Coin:
                EventManager.CollectCoin();
                break;
            case CollectableType.Magnet:
                EventManager.CollectMagnet();
                break;
            case CollectableType.JumpBoots:
                EventManager.CollectJumpBoots();
                break;
            case CollectableType.Jetpack:
                EventManager.CollectJetpack();
                break;
        }
        Destroy(gameObject);
    }
}
