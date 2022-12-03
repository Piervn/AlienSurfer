using System.Collections;
using System.Collections.Generic;

public class EventManager {
    public delegate void CoinAction();
    public static event CoinAction OnCoinCollect;

    public delegate void KeyAction();
    public static event KeyAction OnSpacePress;
    public static event KeyAction OnLeftPress;
    public static event KeyAction OnRightPress;
    public static event KeyAction OnDownPress;

    public static EventManager Instance {
        get; private set;
    }

    /* void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    } */

    public static void CollectCoin() {
        OnCoinCollect?.Invoke();
    }

    public static void SpacePress() {
        OnSpacePress?.Invoke();
    }

    public static void LeftPress() {
        OnLeftPress?.Invoke();
    }

    public static void RightPress() {
        OnRightPress?.Invoke();
    }

    public static void DownPress() {
        OnDownPress?.Invoke();
    }

    
}
