using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioServiseSaveLoader : ISaveLoadObject
{
    private readonly AudioServise _audioServise;
    private readonly SaveLoadServise _saveLoadServise;

    private readonly List<AudioSource> _allMusics;
    private readonly List<AudioSource> _allSounds;

    private readonly string _musicSaveKey = "MusicAudioSave";
    private readonly string _soundSaveKey = "SoundAudioSave";

    public AudioServiseSaveLoader(AudioServise audioServise, SaveLoadServise saveLoadServise, IEnumerable<AudioSource> allMusic, IEnumerable<AudioSource> allSound)
    {
        _audioServise = audioServise;
        _saveLoadServise = saveLoadServise;

        _allMusics = allMusic.ToList();
        _allSounds = allSound.ToList();
    }

    public void Save()
    {
        SaveAudio(_audioServise.CurrentMusic, _allMusics, _musicSaveKey);
        SaveAudio(_audioServise.CurrentSound, _allSounds, _soundSaveKey);
    }

    public void Load()
    {
        if (_saveLoadServise.HasSave(_soundSaveKey))
        {
            LoadAudio(_allMusics, _musicSaveKey);
            LoadAudio(_allSounds, _soundSaveKey);
        }
    }

    private void SaveAudio(AudioSource audioSource, List<AudioSource> audios, string saveKey)
    {
        if (audioSource == null)
        {
            _saveLoadServise.Save<SaveData.AudioData>(saveKey, null);
            return;
        }

        _saveLoadServise.Save(saveKey, new SaveData.AudioData()
        {
            IsMute = audioSource.mute,
            IsPlay = true,
            Volume = audioSource.volume,
            AudioPosition = audios.IndexOf(audioSource)
        });
    }

    private void LoadAudio(List<AudioSource> audioSources, string saveKey)
    {
        var data = _saveLoadServise.Load<SaveData.AudioData>(saveKey);
        if (data == null)
            return;

        AudioSource sound = audioSources[data.AudioPosition];

        sound.mute = data.IsMute;
        sound.volume = data.Volume;

        if (data.IsPlay)
            _audioServise.LoadAudio(sound);
    }
}