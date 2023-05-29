using System;
using System.Collections.Generic;
using Zenject;

namespace Game.RockPaperScissors
{
    public class RockPaperScissorsGame
    {
        public event Action OnDrawn;
        public event Action OnCharacterWon;
        public event Action OnCharacterLost;

        [Inject] private ChoisePanel _choisePanel;       

        private Array _rpsArray = typeof(RockPaperScissorsType).GetEnumValues();
        private string _question = "Начнем, Камень, Ножницы, Бумага!";

        public void StartGame()
        {
            _choisePanel.Show(_question, CreateChoiceElements());
        }

        private List<ChoiseElement> CreateChoiceElements()
        {
            List<ChoiseElement> result = new();

            for (int i = 0; i < _rpsArray.Length; i++)
            {
                RockPaperScissorsType currentType = (RockPaperScissorsType)i;
                result.Add(new ChoiseElement(_rpsArray.GetValue(i).ToString(), () => Compare(currentType)));
            }

            return result;
        }

        private void Compare(RockPaperScissorsType playerGesture)
        {
            RockPaperScissorsType enemyGesture = (RockPaperScissorsType)UnityEngine.Random.Range(0, _rpsArray.Length);

            if (playerGesture == enemyGesture)
            {
                OnDrawn?.Invoke();
                return;
            }

            if (playerGesture == RockPaperScissorsType.Rock)
            {
                switch (enemyGesture)
                {
                    case RockPaperScissorsType.Paper:
                        OnCharacterLost?.Invoke();
                        break;
                    case RockPaperScissorsType.Scissors:
                        OnCharacterWon?.Invoke();
                        break;
                }
            }

            if (playerGesture == RockPaperScissorsType.Paper)
            {
                switch (enemyGesture)
                {
                    case RockPaperScissorsType.Rock:
                        OnCharacterWon?.Invoke();
                        break;
                    case RockPaperScissorsType.Scissors:
                        OnCharacterLost?.Invoke();
                        break;
                }
            }

            if (playerGesture == RockPaperScissorsType.Scissors)
            {
                switch (enemyGesture)
                {
                    case RockPaperScissorsType.Rock:
                        OnCharacterLost?.Invoke();
                        break;
                    case RockPaperScissorsType.Paper:
                        OnCharacterWon?.Invoke();
                        break;
                }
            }
        }
    }
}
