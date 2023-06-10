using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingWindow : MonoBehaviour
{
    [Inject] private AudioServise _audioServise;

    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    public void Show()
    {
        gameObject.SetActive(true);

        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        _musicSlider.onValueChanged.RemoveAllListeners();
        _soundSlider.onValueChanged.RemoveAllListeners();
    }

    private void ChangeMusicVolume(float volume)
    {
        _audioServise.ChangeMusicVolume(volume);
    }

    private void ChangeSoundVolume(float volume)
    {
        _audioServise.ChangeSoundVolume(volume);
    }
}