using System;

public class ContinueStoryCommand : ICommand
{
    private readonly StaticData _staticData;
    private readonly ContinueStoryModel _model;

    public ContinueStoryCommand(StaticData staticData, ContinueStoryModel model)
    {
        _staticData = staticData;
        _model = model;
    }

    public event Action Completed;

    public void Execute()
    {
        if (_staticData.ShowNextStory)
            _model.SetEndPort(_model.NextStoryNode);
        else
            _model.SetEndPort(_model.EndGame);

        Completed?.Invoke();
    }
}