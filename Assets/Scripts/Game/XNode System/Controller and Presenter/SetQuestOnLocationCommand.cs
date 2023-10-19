using System;

public class SetQuestOnLocationCommand : ICommand
{
    public event Action Complete;

    private Map _view;
    private SetQuestOnLocationModel _model;

    public SetQuestOnLocationCommand(Map view, SetQuestOnLocationModel model)
    {
        _view = view;
        _model = model;
    }

    public void Execute()
    {
        _view.SetQuest(_model.LocationType, _model.Quest);
        Complete?.Invoke();
    }
}