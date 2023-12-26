using System;

public class MeetWithPlayerCommand : ICommand
{
    private readonly CharactersLibrary _charactersLibrary;
    private readonly MeetWithPlayerModel _model;

    public event Action Completed;

    public MeetWithPlayerCommand(CharactersLibrary charactersLibrary, MeetWithPlayerModel model)
    {
        _charactersLibrary = charactersLibrary;
        _model = model;
    }

    public void Execute()
    {
        _charactersLibrary.GetCharacter(_model.CharacterType).MeetWithPlayer();
        Completed?.Invoke();
    }
}