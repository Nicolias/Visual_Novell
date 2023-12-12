using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using System.Collections;

public class AudioServise : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _allMusic;
    [SerializeField] private List<AudioSource> _allSound;

    [SerializeField] private float _changeMusicSpeed;

    private AudioServiseSaveLoader _saveLoader;

    [field: SerializeField] public AudioClip CallSound { get; private set; }

    public AudioSource CurrentMusic { get; private set; }
    public AudioSource CurrentSound { get; private set; }

    public AudioServiseSaveLoader SaveLoader => _saveLoader;

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise)
    {
        _saveLoader = new AudioServiseSaveLoader(this, saveLoadServise, _allMusic, _allSound);
    }

    public void LoadAudio(AudioSource audioSource)
    {
        Play(audioSource.clip);
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            CurrentMusic = null;
            CurrentSound = null;

            foreach (var music in _allMusic)
                music.Stop();

            foreach (var audio in _allSound)
                audio.Stop();

            return;
        }

        Play(audioClip);
    }

    public void StopSound(AudioClip audioClip)
    {
        var sound = _allMusic.Find(x => x.clip == audioClip);

        if (sound == null)
        {
            sound = _allSound.Find(x => x.clip == audioClip);
            sound.Stop();

            CurrentSound = null;
        }
        else
        {
            sound.Stop();
            CurrentMusic = null;
        }
    }

    public void ChangeSoundVolume(float volumeLevel)
    {
        ChangeVolume(volumeLevel, _allSound);
    }

    public void ChangeMusicVolume(float volumeLevel)
    {
        ChangeVolume(volumeLevel, _allMusic);
    }

    private void ChangeVolume(float valumeLevel, List<AudioSource> audioSources)
    {
        if (valumeLevel > 1) throw new ArgumentOutOfRangeException("");

        foreach (var audio in audioSources)
            audio.volume = valumeLevel;
    }

    private void Play(AudioClip audioClip)
    {
        var sound = _allMusic.Find(x => x.clip == audioClip);

        if (sound != null)
        {
            StopAllCoroutines();
            StartCoroutine(Change(sound));
        }
        else
        {
            sound = _allSound.Find(x => x.clip == audioClip);
            sound.Play();
            CurrentSound = sound;
        }
    }

    private IEnumerator Change(AudioSource newMusic)
    {
        if (CurrentMusic != null)
        {
            CurrentMusic = newMusic;

            while (AudioListener.volume > 0)
            {
                AudioListener.volume -= 0.01f;
                yield return _changeMusicSpeed * Time.deltaTime;
            }
        }

        AudioListener.volume = 0;

        foreach (var music in _allMusic)
            music.Stop();

        CurrentMusic = newMusic;
        CurrentMusic.Play();

        while (AudioListener.volume < 1)
        {
            AudioListener.volume += 0.1f;
            yield return _changeMusicSpeed * Time.deltaTime;
        }

        AudioListener.volume = 1;
    }
}
