using StateMachine;
using System;

public class ChangeGameStateCommand<T> : ICommand where T : BaseState
{
    private readonly GameStateMachine _gameStateMachine;

    public ChangeGameStateCommand(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public event Action Completed;

    public void Execute()
    {
        _gameStateMachine.ChangeState<T>();
        Completed?.Invoke();
    }
}
