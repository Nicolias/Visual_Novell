using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MiniGameNamespace
{

    public class RockPaperScissors : MiniGame
    {
        [Inject] private CoroutineServise _coroutineServise;

        [SerializeField] private string _winSpeech, _loseSpeech, _drawnSpeech;

        private RockPaperScissorsGame _rockPaperScissorsGame;

        protected override AbstractMiniGame Game => _rockPaperScissorsGame;
        protected override string WinSpeech => _winSpeech;
        protected override string LoseSpeech => _loseSpeech;
        protected override string DrawnSpeech => _drawnSpeech;


        private void Awake()
        {
            _rockPaperScissorsGame = Di.Instantiate<RockPaperScissorsGame>();
        }

        protected override void SetUpGame()
        {
            ChoicePanel.Show("Давай сыграем в Камень, Ножницы, Бумага", new List<ChoiseElement>());
            _coroutineServise.WaitForSecondsAndInvoke(1f, _rockPaperScissorsGame.StartGame);
        }
    }
}