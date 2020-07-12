using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSFX : MonoBehaviour
{
    public static GameSFX Instance;

    private AudioSource _adSrc;

    private void Start()
    {
        Instance = this;
        if (!GetComponent<AudioSource>())
            gameObject.AddComponent<AudioSource>();
        _adSrc = GetComponent<AudioSource>();
        _adSrc.volume = 0.5f;
        _adSrc.loop = false;
        _adSrc.playOnAwake = false;
        _adSrc.clip = null;
    }

    public void PlaySFX(AudioClip sfx, float volume)
    {
        if (sfx != null)
            _adSrc.PlayOneShot(sfx, volume);
    }
}
