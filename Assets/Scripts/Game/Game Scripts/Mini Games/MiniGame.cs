using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MiniGameNamespace
{
    public abstract class MiniGame : MonoBehaviour
    {
        [Inject] protected readonly DiContainer Di;
        [Inject] protected readonly ChoicePanel ChoicePanel;
        [Inject] protected readonly Battery Battery;
        [Inject] private readonly Wallet _wallet;

        private readonly int _sympathyByWin = 3;
        private readonly int _sympathyByDraw = 2;
        private readonly int _sympathyByLose = 1;

        private readonly int _moneyByWin = 5;

        [field: SerializeField] public string GameName { get; private set; }

        protected Character CurrentCharacter { get; private set; }

        protected abstract AbstractMiniGame Game { get; }
        protected abstract string WinSpeech { get; }
        protected abstract string LoseSpeech { get; }
        protected abstract string DrawnSpeech { get; }

        public event Action GameEnded;

        public void StartGame(Character character)
        {
            CurrentCharacter = character;

            Game.CharacterWon += OnGameWin;
            Game.CharacterLost += OnGameLose;
            Game.Drawn += OnGameDrawn;

            SetUpGame();
        }

        protected abstract void SetUpGame();

        private void OnGameWin()
        {
            _wallet.AccureWithOutPanel(_moneyByWin);
            OnGameResult(WinSpeech, _sympathyByWin);
        }

        private void OnGameLose()
        {
            OnGameResult(LoseSpeech, _sympathyByLose);
        }

        private void OnGameDrawn()
        {
            OnGameResult(DrawnSpeech, _sympathyByDraw);
        }

        private void OnGameResult(string resultCharacterSpeech, int sympathyScore)
        {
            CurrentCharacter.AccureSympathyPoints(sympathyScore);

            ChoicePanel.Show(resultCharacterSpeech, new List<ChoiseElement>()
            { new ChoiseElement( "Дальше...", () => EndGame())});
        }

        private void EndGame()
        {
            GameEnded?.Invoke();

            Game.CharacterWon -= OnGameWin;
            Game.CharacterLost -= OnGameLose;
            Game.Drawn -= OnGameDrawn;
        }
    }
}