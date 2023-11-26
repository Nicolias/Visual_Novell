using System;

public class MeetWithPlayerCommand : ICommand
{
    private readonly CharactersLibrary _charactersLibrary;
    private readonly MeetWithPlayerModel _model;

    public event Action Complete;

    public MeetWithPlayerCommand(CharactersLibrary charactersLibrary, MeetWithPlayerModel model)
    {
        _charactersLibrary = charactersLibrary;
        _model = model;
    }

    public void Execute()
    {
        _charactersLibrary.GetCharacter(_model.CharacterType).MeetWithPlayer();
        Complete?.Invoke();
    }
}