using System;
using UnityEngine.SceneManagement;

public class SwitchSceneCommand : ICommand
{
    public event Action OnComplete;

    private int _sceneNumber;

    public SwitchSceneCommand(int sceneNumber)
    {
        _sceneNumber = sceneNumber;
    }

    public void Execute()
    {
        SceneManager.LoadScene(_sceneNumber);
    }
}