public class EatingPastime : AbstractPastime
{
    private Eating _eating;
    private CharactersLibrary _charactersLibrary;

    public EatingPastime(Eating eating, ChoicePanel choicePanel, CharactersLibrary charactersLibrary) : base(choicePanel, "Заказать еду.", eating)
    {
        _eating = eating;
        _charactersLibrary = charactersLibrary;
    }

    public override void Enter(CharacterType character)
    {
        _eating.Enter(_charactersLibrary.GetCharacter(character));
    }
}