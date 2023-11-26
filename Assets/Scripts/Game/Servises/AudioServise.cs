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

    public AudioSource CurrentMusic { get; private set; }
    public AudioSource CurrentSound { get; private set; }

    public AudioServiseSaveLoader SaveLoader => _saveLoader;

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise)
    {
        _saveLoader = new AudioServiseSaveLoader(saveLoadServise, _allMusic, _allSound);
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            CallSound.Stop();

            foreach (var music in _allMusic)
                music.Stop();

            foreach (var audio in _allSound)
                audio.Stop();

            return;
        }

        var sound = _allMusic.Find(x => x.clip == audioClip);

        if (sound != null)
        {
            _allMusic.ForEach(x => x.mute = true);
            sound.mute = false;
            sound.Play();
            CurrentMusic = sound;
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
        }
        else
        {
            sound.Stop();
        }
    }

    public void ChangeSoundVolume(float volumeLevel)
    {
        ChangeVolume(volumeLevel, _allSound);
        CallSound.volume = volumeLevel;
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
}
