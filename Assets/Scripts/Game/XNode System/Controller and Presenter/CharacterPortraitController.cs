using System;

public class CharacterPortraitController : ICommand
{
    public event Action Completed;

    private ICharacterPortraitModel _model;
    private CharacterRenderer _view;

    public CharacterPortraitController(ICharacterPortraitModel model, CharacterRenderer view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        if (_model.PositionType == CharacterPortraitPosition.Delete)
        {
            _view.Delete(_model);
            Completed?.Invoke();
            return;
        }
            
        _view.Show(_model);
        _view.Complite += CallBack;
    }

    private void CallBack()
    {
        _view.Complite -= CallBack;
        Completed?.Invoke();
    }
}
