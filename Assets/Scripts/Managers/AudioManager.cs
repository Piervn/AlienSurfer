using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    public static AudioManager Instance {
        get; private set;
    }

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    

    void Start() {
        Play("Ambient");
        EventManager.OnCoinCollect += () => Play("CoinCollected");
        EventManager.OnBonusCollect += () => Play("BonusCollected");
    }

    public static void Play(string name) {
        Sound s = Array.Find(Instance.sounds, sound => sound.name == name);
        if (s.loop) {
            s.source.Play();
        } else {
            s.source.PlayOneShot(s.clip);
        }
    }
}