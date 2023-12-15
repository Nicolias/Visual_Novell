using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class MenuBehaviour
    {
        private readonly SaveLoadServise _saveLoadServise;
        private readonly List<BaseState> _states;
        private readonly QuitGamePanel _quitGamePanel;
        private BaseState _currentState;

        public MenuBehaviour(ChoiceButton newOrContinueGameButton,
            SettingWindow settingWindow, GameObject menuButtons,
            SaveLoadServise saveLoadServise, QuitGamePanel quitGamePanel)
        {
            _saveLoadServise = saveLoadServise;

            _states = new()
            {
                new NewGameState(newOrContinueGameButton, menuButtons),
                new ContinueGameState(newOrContinueGameButton, menuButtons),
                new SettingState(settingWindow)
            };

            _quitGamePanel = quitGamePanel;

            OpenMainMenu();
        }

        private void OpenMainMenu()
        {
            if (_saveLoadServise.SaveLoadCount > 0)
            {
                Switch<ContinueGameState>();
            }
            else
            {
                Switch<NewGameState>();
            }
        }

        public void OpenSetting()
        {
            Switch<SettingState>();
        }

        public void Quit()
        {
            if (_currentState is SettingState)
                OpenMainMenu();
            else
                _quitGamePanel.Show();
        }

        private void Switch<T>() where T : BaseState
        {
            if (_currentState != null)
                _currentState.Exit();

            var state = _states.Find(x => x is T);
            _currentState = state;
            _currentState.Entry();
        }
    }
}