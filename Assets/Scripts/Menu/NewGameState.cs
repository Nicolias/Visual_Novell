using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameState : BaseState
{
    private ChoiceButton _newGameButton;
    private GameObject _menuButtons;

    private Action _saveAction;
    private SaveLoadServise _saveLoadServise;

    public NewGameState(ChoiceButton newGameButton, GameObject menuButtons, SaveLoadServise saveLoadServise, Action saveAction)
    {
        _newGameButton = newGameButton;
        _menuButtons = menuButtons;

        _saveAction = saveAction;
        _saveLoadServise = saveLoadServise;
    }

    public override void Entry()
    {
        _newGameButton.Initialized(new("Новая игра", () =>
        {
            _saveLoadServise.ClearAllSave();
            _saveAction.Invoke();
            SceneManager.LoadScene(2);
        }));
        _menuButtons.SetActive(true);
    }

    public override void Exit()
    {
        _menuButtons.SetActive(false);
    }
}
