using System.Collections;
using System.Collections.Generic;

public class EventManager {
    public delegate void CollectableAction();
    public static event CollectableAction OnCoinCollect;
    public static event CollectableAction OnBonusCollect;
    public static event CollectableAction OnMagnetCollect;
    public static event CollectableAction OnJumpBootsCollect;
    public static event CollectableAction OnJetpackCollect;

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

    public static void CollectBonus() {
        OnBonusCollect?.Invoke();
    }

    public static void CollectMagnet() {
        OnMagnetCollect?.Invoke();
    }

    public static void CollectJumpBoots() {
        OnJumpBootsCollect?.Invoke();
    }

    public static void CollectJetpack() {
        OnJetpackCollect?.Invoke();
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
        OnMagnetCollect = null;
        OnJumpBootsCollect = null;
        OnJetpackCollect = null;
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
