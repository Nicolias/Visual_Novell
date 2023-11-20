using System;
using UnityEngine;
using UnityEngine.UI;

public class AddSympthyToCharacterController : IController
{
    public event Action Complete;

    private AddSympathyModel _model;
    private CharactersLibrary _charactersLibrary;

    public AddSympthyToCharacterController(AddSympathyModel model, CharactersLibrary charactersLibrary)
    {
        _model = model;
        _charactersLibrary = charactersLibrary;
    }

    public void Execute()
    {
        _charactersLibrary.AddPointsTo(_model.Character, _model.Points);
        Complete?.Invoke();
    }
}
