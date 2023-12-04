using Characters;
using QuizSystem;
using System;

public class Quiz : ICloseable
{
    private CharactersLibrary _characterLibrary;
    private ICharacter _currentCharacter;

    private Wallet _wallet;

    private CharacterType _characterType;
    private bool _canBeClose;
    private bool _isTutorial;

    private QuizView _quizView;

    private int _moneyByWin = 5;

    private int _sympathyPointsByWin = 2;
    private int _sympathyPointsByLose = 0;

    public event Action<int> CharacterSympathyPointsChanged;
    public event Action Closed;

    public Quiz(CharactersLibrary characterLibrary, QuizView quizView, Wallet wallet)
    {
        _characterLibrary = characterLibrary;
        _quizView = quizView;

        _wallet = wallet;

        _quizView.AnswerCorrected += AccureSympathy;
        _quizView.AnswerUncorrected += ResturtQuiz;
    }

    public void Enter(CharacterType characterType, bool canBeClose, bool isTuorial = false)
    {
        _characterType = characterType;
        _canBeClose = canBeClose;
        _isTutorial = isTuorial;

        if (_quizView.CanBeStarted(HideQuiz) == false)
            return;

        if (canBeClose)
        {
            _quizView.ShowCloseButton();
            _quizView.CloseButton.onClick.AddListener(HideQuiz);
        }

        _currentCharacter = _characterLibrary.GetCharacter(characterType);
        _quizView.ShowQuestion(_currentCharacter, isTuorial);
    }

    public void HideQuiz()
    {
        _quizView.HideCanvas();
        Closed?.Invoke();
        _quizView.CloseButton.onClick.RemoveListener(HideQuiz);
    }

    private void AccureSympathy()
    {
        _characterLibrary.AddPointsTo(_characterType, _sympathyPointsByWin);
        _wallet.AccureWithOutPanel(_moneyByWin);

        ResturtQuiz();
    }

    private void ResturtQuiz()
    {
        Enter(_characterType, _canBeClose, _isTutorial);

        CharacterSympathyPointsChanged?.Invoke(_currentCharacter.SympathyPoints);
    }
}
