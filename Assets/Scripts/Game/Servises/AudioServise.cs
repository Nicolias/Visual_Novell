using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class AudioServise : MonoBehaviour, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private List<AudioSource> _allMusic;
    [SerializeField] private List<AudioSource> _allSound;

    [field: SerializeField] public AudioSource CallSound { get; private set; }

    private const string _musicSaveKey = "MusicAudioSave";
    private const string _soundSaveKey = "SoundAudioSave";

    private void Awake()
    {
        if (_saveLoadServise.HasSave(_soundSaveKey))
            Load();
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip == null)
        {
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
        }
        else
        {
            sound = _allSound.Find(x => x.clip == audioClip);
            sound.Play();
        }

        Save();
    }

    internal void StopSound(AudioClip audioClip)
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

        Save();
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
    public void Save()
    {
        SaveAudio(_allMusic, _musicSaveKey);
        SaveAudio(_allSound, _soundSaveKey);
    }

    public void Load()
    {
        LoadAudio(_allMusic, _musicSaveKey);
        LoadAudio(_allSound, _soundSaveKey);
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
