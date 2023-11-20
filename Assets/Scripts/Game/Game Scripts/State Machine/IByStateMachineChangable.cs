namespace StateMachine
{
    public interface IByStateMachineChangable
    {
        public void ChangeBehaviourBy(PlayState playState);
        public void ChangeBehaviourBy(StoryState storyState);
    }
}
