using System;

public class SetQuestOnLocationCommand : ICommand
{
    private SetQuestOnLocationModel _model;

    public SetQuestOnLocationCommand(SetQuestOnLocationModel model)
    {
        _model = model;
    }

    public event Action Complete;

    public void Execute()
    {
        _model.Location.Set(_model.Quest);
        Complete?.Invoke();
    }
}
