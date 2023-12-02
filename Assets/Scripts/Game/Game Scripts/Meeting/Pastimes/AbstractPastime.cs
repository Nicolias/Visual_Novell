using System;

public abstract class AbstractPastime
{
    private readonly ICloseable _objectForClose;

    public AbstractPastime(ChoicePanel choicePanel, string name, ICloseable objectForClose)
    {
        ChoicePanel = choicePanel;
        Name = name;
        _objectForClose = objectForClose;
    }

    public event Action Ended;

    public string Name { get; private set; }
    protected ChoicePanel ChoicePanel { get; private set; }

    public void Enter(CharacterType character)
    {
        _objectForClose.Closed += Exit;

        StartPastime(character);
    }

    protected abstract void StartPastime(CharacterType character);

    private void Exit()
    {
        _objectForClose.Closed -= Exit;
        Ended?.Invoke();
    }
}
