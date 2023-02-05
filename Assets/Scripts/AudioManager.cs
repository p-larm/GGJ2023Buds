using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public Sound windSound;
    public AudioSource musicSource, sfxSource, windSource;
    [SerializeField]
    private float volumeInc = 0.1f;
    [SerializeField]
    private float maxSFXVolume;
    [SerializeField]
    private float maxMusicVolume;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        PlayMusic("Upbeat Theme");
    }

    public void PlayMusic(string name) {
        Sound s = Array.Find(musicSounds, x => x.soundName == name);

        if(s == null) {
            Debug.Log("Sounds Not found");
        }

        else {
            StartCoroutine(FadeMusic(s.clip));
/*             musicSource.clip = s.clip;
            musicSource.Play(); */
        }
    }

    private IEnumerator FadeMusic(AudioClip clip) {
        while(musicSource.volume > 0) {
            musicSource.volume -= volumeInc * Time.deltaTime;
            yield return null;
        }
        musicSource.volume = 0;
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
        while(musicSource.volume < maxMusicVolume) {
            musicSource.volume += volumeInc * Time.deltaTime;
            yield return null;
        }
    }

    public void PlaySFX(string name) {
        Sound s = Array.Find(sfxSounds, x=> x.soundName == name);

        if(s == null) {
            Debug.Log("Sounds Not Found");
        } else {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayWind(AudioSource newWindSource) {
        windSource = newWindSource;
        windSource.clip = windSound.clip;
        windSource.Play();
        windSource.volume = 0;
        StartCoroutine(StartWind());
    }

    public void StopWind() { 
        StartCoroutine(EndWind());
    }

    public IEnumerator StartWind() {
        while(windSource.volume < maxSFXVolume) {
            windSource.volume += volumeInc * Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator EndWind() {
        while(windSource.volume > 0) {
            windSource.volume -= volumeInc * Time.deltaTime;
            yield return null;
        }
        windSource.volume = 0;
        windSource.Stop();
    }

    public void ToggleMusic() {
        musicSource.mute = ! musicSource.mute;
    }

    public void ToggleSFX() {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume) {
        musicSource.volume = volume;
        maxMusicVolume = volume;
    }

    public void SFXVolume(float volume) {
        sfxSource.volume = volume;
        maxSFXVolume = volume;
    }
}
