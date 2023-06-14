using System;
using UnityEngine.SceneManagement;

public class SwitchSceneCommand : ICommand
{
    public event Action OnComplete;

    private readonly SaveLoadServise _saveLoadServise;
    private int _sceneNumber;

    public SwitchSceneCommand(int sceneNumber, SaveLoadServise saveLoadServise)
    {
        _sceneNumber = sceneNumber;
        _saveLoadServise = saveLoadServise;
    }

    public void Execute()
    {
        _saveLoadServise.ClearAllSave();
        SceneManager.LoadScene(_sceneNumber);
    }
}