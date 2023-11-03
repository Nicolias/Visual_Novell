using Characters;
using QuizSystem;
using System;

public class Quiz : ICloseable
{
    private CharactersLibrary _characterLibrary;
    private Character _currentCharacter;

    private CharacterType _characterType;

    private QuizView _quizView;

    private int _sympathyPointsByWin = 2;

    private int _sympathyPointsByLose = 1;

    public event Action<int> CharacterSympathyPointsChanged;
    public event Action Closed;

    public Quiz(CharactersLibrary characterLibrary, QuizView quizView)
    {
        _characterLibrary = characterLibrary;

        _quizView = quizView;

        _quizView.AnswerCorrected += AccureSympathy;
        _quizView.AnswerUncorrected += DecreesSympathy;
    }

    public void HideQuiz()
    {
        _quizView.HideCanvas();
        Closed?.Invoke();
        _quizView.CloseButton.onClick.RemoveListener(HideQuiz);
    }

    public void Enter(CharacterType characterType, bool canBeClose)
    {
        if (canBeClose)
        {
            _quizView.ShowCloseButton();
            _quizView.CloseButton.onClick.AddListener(HideQuiz);
        }

        _characterType = characterType;
        _currentCharacter = _characterLibrary.GetCharacter(characterType);
        _quizView.ShowQuestion(_currentCharacter);
    }

    private void AccureSympathy()
    {
        _characterLibrary.AddPointsTo(_characterType, _sympathyPointsByWin);

        _quizView.ShowQuestion(_currentCharacter);

        CharacterSympathyPointsChanged?.Invoke(_currentCharacter.SympathyPoints);
    }

    private void DecreesSympathy()
    {
        _characterLibrary.DecreesPointsFrom(_characterType, _sympathyPointsByLose);

        _quizView.ShowQuestion(_currentCharacter);

        CharacterSympathyPointsChanged?.Invoke(_currentCharacter.SympathyPoints);
    }
}
