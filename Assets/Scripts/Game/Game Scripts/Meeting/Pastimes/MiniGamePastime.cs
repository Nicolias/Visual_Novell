public class MiniGamePastime : AbstractPastime
{
    private MiniGameSelector _miniGameSelector;

    public MiniGamePastime(MiniGameSelector miniGameSelector, ChoicePanel choicePanel) 
        : base(choicePanel, "Мини игры", miniGameSelector)
    {
        _miniGameSelector = miniGameSelector;
    }

    protected override void StartPastime(CharacterType character)
    {
        _miniGameSelector.Enter(character);
    }
}