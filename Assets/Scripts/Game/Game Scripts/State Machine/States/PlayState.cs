namespace StateMachine
{
    public class PlayState : BaseState
    {
        public override void Accept(IGameStateVisitor gameStateVisitor)
        {
            gameStateVisitor.Visit(this);
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {

        }
    }
}
