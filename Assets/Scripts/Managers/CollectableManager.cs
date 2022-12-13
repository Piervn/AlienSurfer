using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    GameManager gm;
    PlayerMovement pm;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        pm = gameObject.GetComponent<PlayerMovement>();
        EventManager.OnCoinCollect += () => {
            gm.scoreText.text = (++gm.score).ToString();
        };
        EventManager.OnMagnetCollect += () => {
            CollectingBonus(typeof(Magnet));
        };
        EventManager.OnJumpBootsCollect += () => {
            CollectingBonus(typeof(JumpBoots));
        };
        EventManager.OnJetpackCollect += () => {
            CollectingBonus(typeof(Jetpack));
        };
    }

    void CollectingBonus(System.Type bonus) {
        if (gameObject.GetComponent(bonus)) {
            Destroy(gameObject.GetComponent(bonus));
        }
        gameObject.AddComponent(bonus);
    }
}
