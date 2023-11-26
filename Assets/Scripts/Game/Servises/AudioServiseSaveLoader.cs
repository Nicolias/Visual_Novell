using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioServiseSaveLoader : ISaveLoadObject
{
    private readonly SaveLoadServise _saveLoadServise;

    private readonly List<AudioSource> _allMusics;
    private readonly List<AudioSource> _allSounds;

    private readonly string _musicSaveKey = "MusicAudioSave";
    private readonly string _soundSaveKey = "SoundAudioSave";

    public AudioServiseSaveLoader(SaveLoadServise saveLoadServise, IEnumerable<AudioSource> allMusic, IEnumerable<AudioSource> allSound)
    {
        _saveLoadServise = saveLoadServise;
        _allMusics = allMusic.ToList();
        _allSounds = allSound.ToList();
    }

    public void Save()
    {
        SaveAudio(_allMusics, _musicSaveKey);
        SaveAudio(_allSounds, _soundSaveKey);
    }

    public void Load()
    {
        if (_saveLoadServise.HasSave(_soundSaveKey))
        {
            LoadAudio(_allMusics, _musicSaveKey);
            LoadAudio(_allSounds, _soundSaveKey);
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