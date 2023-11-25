using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using Agava.WebUtility;

public class AudioServise : MonoBehaviour, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private List<AudioSource> _allMusic;
    [SerializeField] private List<AudioSource> _allSound;

    [field: SerializeField] public AudioSource CallSound { get; private set; }

    private const string _musicSaveKey = "MusicAudioSave";
    private const string _soundSaveKey = "SoundAudioSave";

    private int _isSilensCount = 0;

    public AudioSource CurrentMusic { get; private set; }
    public AudioSource CurrentSound { get; private set; }

    private void OnEnable()
    {
        Add();
        
        WebApplication.InBackgroundChangeEvent += Silence;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= Silence;
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

    public void Silence(bool silence)
    {
        if (silence == false && _isSilensCount > 0)
            _isSilensCount--;

        if (_isSilensCount == 0)
        {
            AudioListener.pause = silence;
            AudioListener.volume = silence ? 0 : 1;
        }

        if (silence == true)
            _isSilensCount++;
    }

    public void Add()
    {
        _saveLoadServise.Add(this);
    }

    public void Save()
    {
        SaveAudio(_allMusic, _musicSaveKey);
        SaveAudio(_allSound, _soundSaveKey);
    }

    public void Load()
    {
        if (_saveLoadServise.HasSave(_soundSaveKey))
        {
            LoadAudio(_allMusic, _musicSaveKey);
            LoadAudio(_allSound, _soundSaveKey);
        }
    }

    private void SaveAudio(List<AudioSource> audioSources, string saveKey)
    {
        _saveLoadServise.Save(saveKey, new SaveData.IntData() { Int = audioSources.Count });

        for (int i = 0; i < audioSources.Count; i++)
        {
            _saveLoadServise.Save($"{saveKey}/{i}", new SaveData.AudioData()
            {
                IsMute = audioSources[i].mute,
                IsPlay = audioSources[i].isPlaying,
                Volume = audioSources[i].volume
            });
        }
    }

    private void LoadAudio(List<AudioSource> audioSources, string saveKey)
    {
        int count = _saveLoadServise.Load<SaveData.IntData>(saveKey).Int;

        for (int i = 0; i < count; i++)
        {
            var data = _saveLoadServise.Load<SaveData.AudioData>($"{saveKey}/{i}");
            audioSources[i].mute = data.IsMute;

            if (data.IsPlay)
                audioSources[i].Play();
            else
                audioSources[i].Stop();

            audioSources[i].volume = data.Volume;
        }
    }
}