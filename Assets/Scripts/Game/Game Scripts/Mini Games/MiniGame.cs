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
            OnGameResult(WinSpeech);
        }

        private void OnGameLose()
        {
            OnGameResult(LoseSpeech);
        }

        private void OnGameDrawn()
        {
            OnGameResult(DrawnSpeech);
        }

        private void OnGameResult(string resultCharacterSpeech)
        {
            CurrentCharacter.AccureSympathyPoints(1);

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