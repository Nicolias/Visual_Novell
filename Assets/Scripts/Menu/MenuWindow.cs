using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MiniGameNamespace
{
    public class MenuWindow : MonoBehaviour
    {
        [Inject] private AudioServise _audioServise;
        [Inject] private SaveLoadServise _saveLoadServise;

        [SerializeField] private ChoiceButton _newOrContinueGameButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _quitButton;

        [SerializeField] private SettingWindow _settingWindow;
        [SerializeField] private GameObject _menuButtons;

        [SerializeField] private QuitGamePanel _quitGamePanel;

        [SerializeField] private AudioClip _menuMusic;

        private MenuBehaviour _menuBehaviour;

        private void Awake()
        {
            _menuBehaviour = new(_newOrContinueGameButton, _settingWindow, _menuButtons, _saveLoadServise, _quitGamePanel);
        }

        private void OnEnable()
        {
            _settingButton.onClick.AddListener(_menuBehaviour.OpenSetting);
            _quitButton.onClick.AddListener(_menuBehaviour.Quit);

            _audioServise.PlaySound(null);
            _audioServise.PlaySound(_menuMusic);
        }

        private void OnDisable()
        {
            _settingButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }
    }
}