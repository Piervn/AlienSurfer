using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void CoinAction();
    public static event CoinAction OnCoinCollect;

    public static EventManager Instance {
        get; private set;
    }


    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public static void CollectCoin() {
        OnCoinCollect?.Invoke();
    }


}
