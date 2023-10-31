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

    [Inject] private QuestionFactory _questionFactory;

    [SerializeField] private TMP_Text _currentSympathyText;
    [SerializeField] private ChoicePanel _choisePanel;

    [SerializeField] private Canvas _canvas;

    [SerializeField] private float _showTime;
    [SerializeField] private Sprite _uncorrectButtonSprite;

    private Character _currentCharacter;
    private List<ChoiceButton> _uncorrectButtons;

    public void HideCanvas()
    {
        _canvas.enabled = false;
        _choisePanel.Hide();
        _currentCharacter.SympathyPointsChanged -= UpdateSympathyPointsText;
    }

    public void ShowQuestion(Character character)
    {
        _canvas.enabled = true;
        _currentCharacter = character;

        _currentCharacter.SympathyPointsChanged += UpdateSympathyPointsText;
        _currentSympathyText.text = _currentCharacter.SympathyPoints.ToString();

        QuizElement quizElement = _questionFactory.GetQuestion(character.Type);
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
            uncorrectButton.Button.image.sprite = _uncorrectButtonSprite;

        yield return new WaitForSeconds(_showTime);

        action.Invoke();
    }
}
