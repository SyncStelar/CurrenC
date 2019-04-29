using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public static SoundController sc;

    private void Awake() {
        sc = this;
    }

    public AudioClip chaChing;

    public void PlaySound(AudioClip sound) {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = sound;
        audio.PlayOneShot(sound);
    }
}
