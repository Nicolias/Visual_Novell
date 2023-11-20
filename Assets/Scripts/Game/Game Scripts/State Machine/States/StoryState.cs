namespace StateMachine
{
    public class StoryState : BaseState
    {
        private Smartphone _smartphone;

        public StoryState(Smartphone smartphone)
        {
            _smartphone = smartphone;
        }

        public override void Accept(IGameStateVisitor gameStateVisitor)
        {
            gameStateVisitor.Visit(this);
        }

        public override void Enter()
        {
            ChangeEnableInSmartphone(false);
        }

        public override void Exit()
        {
            ChangeEnableInSmartphone(true);
        }

        private void ChangeEnableInSmartphone(bool isEnabled)
        {
            Dictionary.Dictionary<SmartphoneWindows, bool> appsForChangeEnable = new Dictionary.Dictionary<SmartphoneWindows, bool>();

            appsForChangeEnable.Add(SmartphoneWindows.Map, isEnabled);
            appsForChangeEnable.Add(SmartphoneWindows.DUX, isEnabled);
            appsForChangeEnable.Add(SmartphoneWindows.Contacts, isEnabled);

            _smartphone.ChangeEnabled(appsForChangeEnable);
        }
    }
}
