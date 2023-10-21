using Game.RockPaperScissors;
using UnityEngine;
using Zenject;

public class RockPaperScissors : MiniGame<RockPaperScissorsGame>
{
    [Inject] private CoroutineServise _coroutineServise;

    [SerializeField] private string _winSpeech, _loseSpeech, _drawnSpeech;

    protected override string WinSpeech => _winSpeech;
    protected override string LoseSpeech => _loseSpeech;
    protected override string DrawnSpeech => _drawnSpeech;

    protected override void SetUpGame()
    {
        ChoicePanel.Show("Давай сыграем в Камень, Ножницы, Бумага", new());
        _coroutineServise.WaitForSecondsAndInvoke(1f, Game.StartGame);        
    }    
}
