using System;

public class QuizPresenter : ICommand
{
    public event Action Complete;

    private readonly QuizModel _quizModel;
    private readonly Quiz _quiz;

    public QuizPresenter(QuizModel quizModel, Quiz quiz)
    {
        _quizModel = quizModel;
        _quiz = quiz;
    }

    public void Execute()
    {
        _quiz.StartQuiz(_quizModel.CharacterType);
        _quiz.CharacterSympathyPointsChanged += CallBack;
    }

    private void CallBack(int sympathyPoints)
    {
        if (sympathyPoints >= _quizModel.PointsGoal)
        {
            _quiz.CharacterSympathyPointsChanged -= CallBack;
            _quiz.HideQuiz();
            Complete?.Invoke();
        }
    }
}
