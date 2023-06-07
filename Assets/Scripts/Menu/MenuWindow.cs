using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuWindow : MonoBehaviour
{
    [SerializeField] private ChoiceButton _newOrContinueGameButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _quitButton;

    [SerializeField] private SettingWindow _settingWindow;
    [SerializeField] private GameObject _menuButtons;

    [SerializeField] private TMP_Text _timeText;

    [SerializeField] private Image _background;
    [SerializeField] private Sprite _morningSprite;
    [SerializeField] private Sprite _daySprite;
    [SerializeField] private Sprite _eveningSprite;
    [SerializeField] private Sprite _nightSprite;

    private SceneLoader _sceneLoader;
    private TimesOfDayServise _timesOfDayServise;

    private MenuBehaviour _menuBehaviour;

    [Inject]
    public void Construct(SceneLoader sceneLoader, TimesOfDayServise timesOfDayServise)
    {
        _timesOfDayServise = timesOfDayServise;
        _sceneLoader = sceneLoader;
    }

    private void Awake()
    {
        _menuBehaviour = new(_newOrContinueGameButton, _settingWindow, _menuButtons, _sceneLoader);
    }

    private void OnEnable()
    {
        _settingButton.onClick.AddListener(_menuBehaviour.OpenSetting);
        _quitButton.onClick.AddListener(_menuBehaviour.Quit);
        ChangeBackground(_timesOfDayServise.GetCurrentTimesOfDay());
    }

    private void OnDisable()
    {
        _settingButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        _menuBehaviour.OpenMainMenu();
    }

    private void Update()
    {
        _timeText.text = $"{_timesOfDayServise.CurrentTime.Hour}:{_timesOfDayServise.CurrentTime.Minute}";
    }

    private void ChangeBackground(TimesOfDayType timesOfDayType)
    {
        switch (timesOfDayType)
        {
            case TimesOfDayType.Morning:
                _background.sprite = _morningSprite;
                break;
            case TimesOfDayType.Day:
                _background.sprite = _daySprite;
                break;
            case TimesOfDayType.Evning:
                _background.sprite = _eveningSprite;
                break;
            case TimesOfDayType.Night:
                _background.sprite = _nightSprite;
                break;
            default:
                break;
        }
    }
}