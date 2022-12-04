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
        }
    }

    

    void Start() {
        EventManager.OnCoinCollect += () => Play("CoinCollected");
    }

    public static void Play(string name) {
        Sound s = Array.Find(Instance.sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.clip);
    }
}