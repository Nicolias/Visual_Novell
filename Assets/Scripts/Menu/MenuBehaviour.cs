using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour
{
    private readonly List<BaseState> _states;
    private BaseState _currentState;

    private bool _hasSave;

    public MenuBehaviour(ChoiceButton newOrContinueGameButton, 
        SettingWindow settingWindow, GameObject menuButtons, SceneLoader sceneLoader)
    {
        _states = new()
        {
            new NewGameState(newOrContinueGameButton, menuButtons, sceneLoader),
            new ContinueGameState(newOrContinueGameButton, menuButtons, sceneLoader),
            new SettingState(settingWindow)
        };
    }

    public void OpenMainMenu()
    {
        if (_hasSave)
            Switch<ContinueGameState>();
        else
            Switch<NewGameState>();
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
            Application.Quit();
    }

    private void Switch<T>() where T : BaseState
    {
        if(_currentState != null)
            _currentState.Exit();

        var state = _states.Find(x => x is T);
        _currentState = state;
        _currentState.Entry();
    }
}
