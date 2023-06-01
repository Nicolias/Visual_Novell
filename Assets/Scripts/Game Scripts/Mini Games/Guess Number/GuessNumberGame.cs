using System;
using UnityEngine;
using Zenject;

namespace Game.GuessNumber
{
    public class GuessNumberGame
    {
        public event Action OnDrawn;
        public event Action OnCharacterWon;
        public event Action OnCharacterLost;

        [Inject] private readonly CoroutineServise _coroutineServise;
        [Inject] private readonly ChoicePanel _choicePanel;

        private GuessNumberPanel _guessNumberPanel;
        private (int, int) _numberBounds;

        public void StartGame(GuessNumberPanel guessNumberPanel, (int, int) numberBounds)
        {
            _guessNumberPanel = guessNumberPanel;

            _numberBounds = numberBounds;

            _guessNumberPanel.OpenPanel(numberBounds, $"Выбири число от {numberBounds.Item1} до {numberBounds.Item2}");
            _guessNumberPanel.OnValueSelected += Compare;
        }

        private void Compare(int playerNumber)
        {
            _guessNumberPanel.OnValueSelected -= Compare;
            int enemyNumber = UnityEngine.Random.Range(_numberBounds.Item1, _numberBounds.Item2);

            _choicePanel.Show($"Противник выбрал число {enemyNumber}", new());

            _coroutineServise.WaitForSecondsAndInvoke(1.5f, () =>
            {
                if (enemyNumber == playerNumber)
                    OnCharacterWon?.Invoke();
                else
                    OnCharacterLost?.Invoke();
            });
        }
    }
}