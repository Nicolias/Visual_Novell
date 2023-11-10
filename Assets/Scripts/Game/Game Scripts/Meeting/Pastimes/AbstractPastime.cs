using System;

public abstract class AbstractPastime
{
    private readonly ICloseable _objectForClose;

    public AbstractPastime(ChoicePanel choicePanel, string name, ICloseable objectForClose)
    {
        ChoicePanel = choicePanel;
        Name = name;
        _objectForClose = objectForClose;

        _objectForClose.Closed += Exit;
    }

    public event Action Ended;

    public string Name { get; private set; }
    protected ChoicePanel ChoicePanel { get; private set; }

    public abstract void Enter(CharacterType character);

    private void Exit()
    {
        _objectForClose.Closed -= Exit;
        Ended?.Invoke();
    }
}
