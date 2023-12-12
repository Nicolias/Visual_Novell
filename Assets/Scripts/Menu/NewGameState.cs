using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class NewGameState : BaseState
    {
        private ChoiceButton _newGameButton;
        private GameObject _menuButtons;

        public NewGameState(ChoiceButton newGameButton, GameObject menuButtons)
        {
            _newGameButton = newGameButton;
            _menuButtons = menuButtons;
        }

        public override void Entry()
        {
            _newGameButton.Initialized(new("Новая игра", () =>
            {
                SceneManager.LoadScene(1);
            }));
            _menuButtons.SetActive(true);
        }

        public override void Exit()
        {
            _menuButtons.SetActive(false);
        }
    }
}