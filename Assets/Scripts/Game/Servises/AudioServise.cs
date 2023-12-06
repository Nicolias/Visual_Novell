using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class AudioServise : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _allMusic;
    [SerializeField] private List<AudioSource> _allSound;

    [field: SerializeField] public AudioSource CallSound { get; private set; }

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

        if (sound != null)
        {
            StartCoroutine(Change(sound));
        }
        else
        {
            sound = _allSound.Find(x => x.clip == audioClip);
            sound.Play();
            CurrentSound = sound;
        }
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

    private IEnumerator Change(AudioSource sound)
    {
        while (AudioListener.volume > 0)
        {
            AudioListener.volume -= _changeMusicSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        AudioListener.volume = 0;

        if (CurrentMusic != null)
            CurrentMusic.mute = true;

        sound.mute = false;
        sound.Play();
        CurrentMusic = sound;

        while (AudioListener.volume < 1)
        {
            AudioListener.volume += _changeMusicSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        AudioListener.volume = 1;
    }
}
