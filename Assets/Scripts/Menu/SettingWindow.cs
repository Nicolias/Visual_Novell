using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingWindow : MonoBehaviour
{
    [Inject] private AudioServise _audioServise;

    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    private void OnEnable()
    {
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
    }

    private void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveAllListeners();
        _soundSlider.onValueChanged.RemoveAllListeners();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
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