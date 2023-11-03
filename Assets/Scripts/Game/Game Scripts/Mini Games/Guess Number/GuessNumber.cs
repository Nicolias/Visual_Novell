using System.Collections.Generic;
using UnityEngine;

namespace MiniGameNamespace
{
    public class GuessNumber : MiniGame
    {
        [SerializeField] private string _winSpeech, _loseSpeech, _drawnSpeech;
        [SerializeField] private GuessNumberPanel _guessNumberPanel;

        private GuessNumberGame _guessNumberGame;
        private List<(int, int)> _boundsNumbersForGame = new()
        {
            new(1, 3),
            new(1, 10)
        };
        private string _currentWinSpeech;

        protected override AbstractMiniGame Game => _guessNumberGame;
        protected override string WinSpeech => _currentWinSpeech;
        protected override string LoseSpeech => _loseSpeech;
        protected override string DrawnSpeech => _drawnSpeech;

        private void Awake()
        {
            _guessNumberGame = Di.Instantiate<GuessNumberGame>();
        }

        protected override void SetUpGame()
        {
            ChoicePanel.Show("Давай сыграем в угадай число", GetChoiceElements(_boundsNumbersForGame));
        }

        private List<ChoiseElement> GetChoiceElements(List<(int, int)> boundsVariation)
        {
            List<ChoiseElement> choiseElements = new();

            foreach (var bounds in boundsVariation)
            {
                choiseElements.Add(
                    new($"Выбрать число от {bounds.Item1} до {bounds.Item2}",
                    () =>
                    {
                        _guessNumberGame.StartGame(_guessNumberPanel, bounds);
                        ChoicePanel.Hide();
                        _currentWinSpeech = _winSpeech.Replace("(2)", bounds.Item2.ToString());
                    }));
            }

            return choiseElements;
        }
    }
}