using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class ContinueGameState : BaseState
    {
        private ChoiceButton _newGameButton;
        private GameObject _menuButtons;

        public ContinueGameState(ChoiceButton newGameButton, GameObject menuButtons)
        {
            _newGameButton = newGameButton;
            _menuButtons = menuButtons;
        }

        public override void Entry()
        {
            _newGameButton.Initialized(new("Продолжить", () => SceneManager.LoadScene(1)));
            _menuButtons.SetActive(true);
        }

        public override void Exit()
        {
            _menuButtons.SetActive(false);
        }
    }
}