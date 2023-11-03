using System;
using System.Collections.Generic;
using Zenject;

namespace MiniGameNamespace
{
    public class GuessNumberGame : AbstractMiniGame
    {
        [Inject] private readonly CoroutineServise _coroutineServise;
        [Inject] private readonly ChoicePanel _choicePanel;

        private GuessNumberPanel _guessNumberPanel;
        private (int, int) _numberBounds;

        public override event Action Drawn;
        public override event Action CharacterWon;
        public override event Action CharacterLost;

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

            _choicePanel.Show($"Соперник выбрал число {enemyNumber}", new List<ChoiseElement>());

            _coroutineServise.WaitForSecondsAndInvoke(1.5f, () =>
            {
                if (enemyNumber == playerNumber)
                    CharacterWon?.Invoke();
                else
                    CharacterLost?.Invoke();
            });
        }
    }
}