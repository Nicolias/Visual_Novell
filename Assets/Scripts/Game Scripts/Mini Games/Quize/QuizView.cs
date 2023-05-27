using Factory.Quiz;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class QuizView : MonoBehaviour
{
    public event Action OnAnswerCorrected;
    public event Action OnAnswerUncorrected;

    [SerializeField] private TMP_Text _currentSympathyText;
    [SerializeField] private ChoisePanel _choisePanel;

    [Inject] private QuestionFactory _questionFactory;

    public void ShowQuestion(CharacterType characterType)
    {
        QuizElement quizElement = _questionFactory.GetQuestion(characterType);
        _choisePanel.Show(quizElement.Qustion, GenerateChoicElements(quizElement));       
    }

    private List<ChoiseElement> GenerateChoicElements(QuizElement quizElement)
    {
        List<ChoiseElement> choiseElements = new();

        for (int i = 0; i < quizElement.UncorrectedAnswerText.Count + 1; i++)
            choiseElements.Add(new(quizElement.UncorrectedAnswerText[i], () => OnAnswerUncorrected?.Invoke()));

        choiseElements.Add(new(quizElement.CorrectAnswerText, () => OnAnswerCorrected?.Invoke()));

        return choiseElements;
    }
}
