using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : ISaveLoadObject
{
    private readonly SaveLoadServise _saveLoadServise;
    private readonly List<BaseState> _states;
    private BaseState _currentState;

    private const string _saveKey = "HasGameProgress";

    public MenuBehaviour(ChoiceButton newOrContinueGameButton, 
        SettingWindow settingWindow, GameObject menuButtons, SaveLoadServise saveLoadServise)
    {
        _saveLoadServise = saveLoadServise;

        _states = new()
        {
            new NewGameState(newOrContinueGameButton, menuButtons, saveLoadServise, Save),
            new ContinueGameState(newOrContinueGameButton, menuButtons),
            new SettingState(settingWindow)
        };

        Load();
    }

    public void OpenMainMenu()
    {
        if (_saveLoadServise.HasSave(_saveKey))
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

    public void Load()
    {
        OpenMainMenu();
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.BoolData() { Bool = true });
    }
}
