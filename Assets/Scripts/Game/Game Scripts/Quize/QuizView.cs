using Characters;
using Factory.Quiz;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace QuizSystem
{
    public class QuizView : MonoBehaviour
    {
        [Inject] private QuestionFactory _questionFactory;
        [Inject] private Battery _battery;

        [SerializeField] private TMP_Text _currentChargeLevelText;
        [SerializeField] private TMP_Text _currentSympathyText;
        [SerializeField] private ChoicePanel _choisePanel;

        [SerializeField] private Canvas _canvas;

        [SerializeField] private float _showTime;
        [SerializeField] private Sprite _uncorrectButtonSprite;

        [SerializeField] private Button _closeButton;

        private int _startQuizCost = 5;

        private ICharacter _currentCharacter;
        private List<ChoiceButton> _uncorrectButtons;

        private bool _isShowUncorrectUnswer;

        public Button CloseButton => _closeButton;

        public event Action AnswerCorrected;
        public event Action AnswerUncorrected;

        public void HideCanvas()
        {
            if (_canvas.enabled == false) return;

            _closeButton.gameObject.SetActive(false);

            _canvas.enabled = false;
            _choisePanel.Hide();
            _currentCharacter.SympathyPointsChanged -= UpdateSympathyPointsText;
        }

        public bool CanBeStarted(Action hideCanvas)
        {
            if (_battery.CurrentValue < _startQuizCost)
            {
                _choisePanel.Show("Недостаточно энергии", new List<ChoiseElement>()
                {
                    new ChoiseElement("Принять", hideCanvas)
                });
                return false;
            }

            return true;
        }

        public void ShowQuestion(ICharacter character, bool isTutorial)
        {
            if (isTutorial == false)
                _battery.Decreese(_startQuizCost);

            _currentChargeLevelText.text = "Осталось энергии: " + _battery.CurrentValue.ToString();

            _canvas.enabled = true;
            _currentCharacter = character;

            _currentCharacter.SympathyPointsChanged += UpdateSympathyPointsText;
            _currentSympathyText.text = _currentCharacter.SympathyPoints.ToString();

            QuizQuestion quizElement = _questionFactory.GetQuestion(character.Type);
            _uncorrectButtons = _choisePanel.ShowAndGetUncorrectButtons(quizElement.Qustion, GenerateChoicElements(quizElement));
        }

        public void ShowCloseButton()
        {
            _closeButton.gameObject.SetActive(true);
        }

        private List<(AnswerType, ChoiseElement)> GenerateChoicElements(QuizQuestion quizElement)
        {
            List<(AnswerType, ChoiseElement)> choiseElements = new List<(AnswerType, ChoiseElement)>();

            for (int i = 0; i < quizElement.UncorrectedAnswerText.Count; i++)
                choiseElements.Add((AnswerType.Uncorrect, new ChoiseElement(quizElement.UncorrectedAnswerText[i], () =>
                {
                    StartCoroutine(OnUncorrectAnswer());
                })));

            choiseElements.Add((AnswerType.Correct, new ChoiseElement(quizElement.CorrectAnswerText, () =>
            {
                StartCoroutine(OnCorrectAnswer());
            })));

            choiseElements.Shuffle();

            return choiseElements;
        }

        private void UpdateSympathyPointsText(int points)
        {
            _currentSympathyText.text = points.ToString();
        }

        private IEnumerator OnCorrectAnswer()
        {
            if (_isShowUncorrectUnswer == false)
            {
                yield return ShowUncorrectButtons();

                AnswerCorrected?.Invoke();
            }
        }

        private IEnumerator OnUncorrectAnswer()
        {
            if (_isShowUncorrectUnswer == false)
            {
                yield return ShowUncorrectButtons();

                AnswerUncorrected?.Invoke();
            }
        }

        private IEnumerator ShowUncorrectButtons()
        {
            _isShowUncorrectUnswer = true;

            foreach (var uncorrectButton in _uncorrectButtons)
                uncorrectButton.Button.image.sprite = _uncorrectButtonSprite;

            yield return new WaitForSeconds(_showTime);

            _choisePanel.Hide();
            _isShowUncorrectUnswer = false;
        }
    }
}