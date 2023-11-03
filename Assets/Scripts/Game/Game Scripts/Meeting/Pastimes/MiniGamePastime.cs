public class MiniGamePastime : AbstractPastime
{
    private MiniGameSelector _miniGameSelector;

    public MiniGamePastime(MiniGameSelector miniGameSelector, ChoicePanel choicePanel) 
        : base(choicePanel, "Мини игры", miniGameSelector)
    {
        _miniGameSelector = miniGameSelector;
    }

    public override void Enter(CharacterType character)
    {
        _miniGameSelector.Enter(character);
        _miniGameSelector.Closed += Exit;
    }
}