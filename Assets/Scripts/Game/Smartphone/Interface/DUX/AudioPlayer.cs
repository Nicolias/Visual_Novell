using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AudioPlayer : MonoBehaviour
{
    [Inject] private AudioServise _audioServise;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _pauseButton;

    [SerializeField] private Slider _progerssSlider;

    private float _currentProgress;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(() =>
        {
            _audioSource.Play();

            if (_currentProgress == _progerssSlider.maxValue)
                _currentProgress = _progerssSlider.minValue;

            _audioSource.time = _currentProgress * _audioSource.clip.length;
        });

        _pauseButton.onClick.AddListener(() => _audioSource.Stop());

        _progerssSlider.onValueChanged.AddListener((x) => _currentProgress = x);

        _audioServise.CurrentMusic.Pause();
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
        _pauseButton.onClick.RemoveAllListeners();

        _progerssSlider.onValueChanged.RemoveAllListeners();

        _audioServise.CurrentMusic.Play();
    }

    private void Update()
    {
        if (_audioSource.clip != null && _audioSource.isPlaying)
        {
            _currentProgress = _audioSource.time / _audioSource.clip.length;
            _progerssSlider.value = _currentProgress;

            if (_currentProgress == _progerssSlider.maxValue)
                _currentProgress = _progerssSlider.minValue;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _audioSource.clip = null;
        gameObject.SetActive(false);
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Pause();
        _progerssSlider.value = _progerssSlider.minValue;
        _currentProgress = _progerssSlider.minValue;
    }
}