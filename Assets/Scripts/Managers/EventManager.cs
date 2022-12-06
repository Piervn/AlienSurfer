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

    public delegate void PlayerAction();
    public static event PlayerAction OnPlayerLands;
    public static event PlayerAction OnPlayerFalls;

    public delegate void GameAction();
    public static event GameAction OnGameOver;

    public static EventManager Instance {
        get; private set;
    }


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

    public static void PlayerLands() {
        OnPlayerLands?.Invoke();
    }

    public static void PlayerFalls() {
        OnPlayerFalls?.Invoke();
    }

    public static void GameOver() {
        OnGameOver?.Invoke();
    }

    public static void ClearEvents() {
        OnCoinCollect = null;
        OnSpacePress = null;
        OnLeftPress = null;
        OnRightPress = null;
        OnDownPress = null;
        OnPlayerLands = null;
        OnPlayerFalls = null;
        OnGameOver = null;
    }

    public static void ClearInputEvents() {
        OnSpacePress = null;
        OnLeftPress = null;
        OnRightPress = null;
        OnDownPress = null;
    }
    
}
