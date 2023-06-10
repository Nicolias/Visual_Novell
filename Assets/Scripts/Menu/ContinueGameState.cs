﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGameState : BaseState
{
    private ChoiceButton _newGameButton;
    private GameObject _menuButtons;

    private AsyncOperation _async;

    public ContinueGameState(ChoiceButton newGameButton, GameObject menuButtons)
    {
        _newGameButton = newGameButton;
        _menuButtons = menuButtons;
    }

    public override void Entry()
    {
        if (_async == null)
        {
            _async = SceneManager.LoadSceneAsync(1);
            _async.allowSceneActivation = false;
        }

        _newGameButton.Initialized(new("Продолжить", () => _async.allowSceneActivation = true));
        _menuButtons.SetActive(true);
    }

    public override void Exit()
    {
        _menuButtons.SetActive(false); 
    }
}
