using Characters;
using Factory.Quiz;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class QuizView : MonoBehaviour
{
    public event Action OnAnswerCorrected;
    public event Action OnAnswerUncorrected;

    [SerializeField] private float _showTime;

    [SerializeField] private TMP_Text _currentSympathyText;
    [SerializeField] private ChoisePanel _choisePanel;

    [SerializeField] private Canvas _canvas;

    [Inject] private QuestionFactory _questionFactory;

    private Character _currentCharacter;
    private List<ChoiceButton> _uncorrectButtons;

    public void HideCanvas()
    {
        _canvas.enabled = false;
        _choisePanel.Hide();
        _currentCharacter.OnSympathyPointsChanged -= UpdateSympathyPointsText;
    }

    public void ShowQuestion(Character character)
    {
        _canvas.enabled = true;
        _currentCharacter = character;

        _currentCharacter.OnSympathyPointsChanged += UpdateSympathyPointsText;
        _currentSympathyText.text = _currentCharacter.SympathyPoints.ToString();

        QuizElement quizElement = _questionFactory.GetQuestion(character.CharacterType);
        _uncorrectButtons = _choisePanel.ShowAndGetUncorrectButtons(quizElement.Qustion, GenerateChoicElements(quizElement));
    }

    private List<(AnswerType ,ChoiseElement)> GenerateChoicElements(QuizElement quizElement)
    {
        List<(AnswerType, ChoiseElement)> choiseElements = new();

        for (int i = 0; i < quizElement.UncorrectedAnswerText.Count; i++)
            choiseElements.Add((AnswerType.Uncorrect, new(quizElement.UncorrectedAnswerText[i], () =>
            {
                StartCoroutine(ShowUncorrectButtons(() =>
                {
                    _choisePanel.Hide();
                    OnAnswerUncorrected?.Invoke();
                }));
            })));

        choiseElements.Add((AnswerType.Correct, new(quizElement.CorrectAnswerText, () =>
        {
            StartCoroutine(ShowUncorrectButtons(() =>
            {
                _choisePanel.Hide();
                OnAnswerCorrected?.Invoke();
            }));
        })));

        choiseElements.Shuffle();

        return choiseElements;
    }

    private void UpdateSympathyPointsText(int points)
    {
        _currentSympathyText.text = points.ToString();
    }

    private IEnumerator ShowUncorrectButtons(Action action)
    {
        foreach (var uncorrectButton in _uncorrectButtons)
            uncorrectButton.Button.image.color = Color.red;        

        yield return new WaitForSeconds(_showTime);

        action.Invoke();
    }
}
