using System;

public class CharacterPortraitController : IController
{
    public event Action OnComplete;

    private ICharacterPortraitModel _model;
    private ICharacterPortraitView _view;

    public CharacterPortraitController(ICharacterPortraitModel model, ICharacterPortraitView view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.Show(_model.Name, _model.Sprite, _model.Position);
        OnComplete?.Invoke();
    }
}