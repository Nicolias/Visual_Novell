using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioServise : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _allMusic;
    [SerializeField] private List<AudioSource> _allSound;

    public void PlaySound(AudioClip audioClip)
    {
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
    }
}