using System;

public class Quiz
{
    public event Action OnCharacterSympathyPointsChanged;

    private Character _character;
    private QuizView _quizView;

    private int _sympathyPointsByWin = 2;
    private int _sympathyPointsByLose = 1;

    public Quiz(Character character, QuizView quizView)
    {
        _character = character;

        _quizView = quizView;

        _quizView.OnAnswerCorrected += AccureSympathy;
        _quizView.OnAnswerUncorrected += DecreesSympathy;
    }

    private void AccureSympathy()
    {
        _character.AccureSympathyPoints(_sympathyPointsByWin);

        _quizView.ShowQuestion(_character.CharacterType);
    }

    private void DecreesSympathy()
    {
        _character.DecreesSympathyPoints(_sympathyPointsByLose);

        _quizView.ShowQuestion(_character.CharacterType);
    }
}
