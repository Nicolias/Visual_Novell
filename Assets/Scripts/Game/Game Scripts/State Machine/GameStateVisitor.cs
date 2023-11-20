namespace StateMachine
{
    public class GameStateVisitor : IGameStateVisitor
    {
        private GameStateMachine _gameStateMachine;

        private IByStateMachineChangable _stateMachineChangable;

        public GameStateVisitor(GameStateMachine gameStateMachine, IByStateMachineChangable stateMachineChangable)
        {
            _gameStateMachine = gameStateMachine;

            _stateMachineChangable = stateMachineChangable;
        }

        public void SubscribeOnGameStateMachine()
        {
            _gameStateMachine.StateChanged += RecognizeCurrentGameState;
        }

        public void UnsubsciribeFromGameStateMachine()
        {
            _gameStateMachine.StateChanged -= RecognizeCurrentGameState;
        }

        public void RecognizeCurrentGameState()
        {
            _gameStateMachine.CurrentState.Accept(this);
        }

        public void Visit(PlayState playState) => _stateMachineChangable.ChangeBehaviourBy(playState);

        public void Visit(StoryState storyState) => _stateMachineChangable.ChangeBehaviourBy(storyState);
    }
}
