using System;

public class CharacterPortraitController : ICommand
{
    public event Action Complete;

    private ICharacterPortraitModel _model;
    private ICharacterPortraitView _view;

    public CharacterPortraitController(ICharacterPortraitModel model, ICharacterPortraitView view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        if (_model.PositionType == CharacterPortraitPosition.Delete)
        {
            _view.Delete(_model);
            Complete?.Invoke();
            return;
        }
            
        _view.Show(_model);
        _view.Complite += CallBack;
    }

    private void CallBack()
    {
        _view.Complite -= CallBack;
        Complete?.Invoke();
    }
}
