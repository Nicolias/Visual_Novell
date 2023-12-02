public class QuizPastime : AbstractPastime
{
    private Quiz _quiz;

    public QuizPastime(Quiz quiz, ChoicePanel choicePanel) 
        : base(choicePanel, "Виктарина", quiz)
    {
        _quiz = quiz;
    }

    protected override void StartPastime(CharacterType character)
    {
        _quiz.Enter(character, true);
    }
}
