using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    GameManager gm;
    PlayerMovement pm;
    PlayerAnimations pa;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        pm = gameObject.GetComponent<PlayerMovement>();
        EventManager.OnCoinCollect += () => {
            gm.scoreText.text = (++gm.score).ToString();
        };
        EventManager.OnMagnetCollect += () => {
            EventManager.CollectBonus();
            gameObject.AddComponent<Magnet>();
        };
        EventManager.OnJumpBootsCollect += () => {
            EventManager.CollectBonus();
            if (gameObject.GetComponent<JumpBoots>()) {
                Destroy(gameObject.GetComponent<JumpBoots>());
            }
            gameObject.AddComponent<JumpBoots>();
        };
        EventManager.OnJetpackCollect += () => {
            EventManager.CollectBonus();
            if (gameObject.GetComponent<Jetpack>()) {
                Destroy(gameObject.GetComponent<Jetpack>());
            }
            gameObject.AddComponent<Jetpack>();
        };
    }
}
