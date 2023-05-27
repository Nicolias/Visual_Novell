using Characters;
using System;

public class Quiz
{
    public event Action OnCharacterSympathyPointsChanged;

    private CharactersLibrary _characterLibrary;
    private Character _currentCharacter;

    private CharacterType _characterType;

    private QuizView _quizView;

    private int _sympathyPointsByWin = 2;
    private int _sympathyPointsByLose = 1;

    public Quiz(CharactersLibrary characterLibrary, QuizView quizView)
    {
        _characterLibrary = characterLibrary;

        _quizView = quizView;

        _quizView.OnAnswerCorrected += AccureSympathy;
        _quizView.OnAnswerUncorrected += DecreesSympathy;
    }

    public void StartQuiz(CharacterType characterType)
    {
        _characterType = characterType;
        _currentCharacter = _characterLibrary.AllCharacters.Find(x => x.CharacterType == characterType);
        _quizView.ShowQuestion(_currentCharacter);
    }

    private void AccureSympathy()
    {
        _characterLibrary.AddPointsTo(_characterType, _sympathyPointsByWin);

        _quizView.ShowQuestion(_currentCharacter);
    }

    private void DecreesSympathy()
    {
        _characterLibrary.DecreesPointsFrom(_characterType, _sympathyPointsByLose);

        _quizView.ShowQuestion(_currentCharacter);
    }
}
