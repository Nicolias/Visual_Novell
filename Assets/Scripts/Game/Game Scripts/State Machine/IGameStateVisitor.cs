namespace StateMachine
{
    public interface IGameStateVisitor
    {
        void SubscribeOnGameStateMachine();
        void UnsubsciribeFromGameStateMachine();

        void RecognizeCurrentGameState();
        void Visit(PlayState playState);
        void Visit(StoryState storyState);
    }
}
