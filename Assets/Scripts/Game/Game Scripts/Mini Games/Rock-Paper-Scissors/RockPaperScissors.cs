using Characters;
using Game.RockPaperScissors;
using System;
using UnityEngine;
using Zenject;

public class RockPaperScissors : MiniGame
{
    [Inject] private DiContainer _di;
    [Inject] private CoroutineServise _coroutineServise;

    [SerializeField] private string _winSpeech, _loseSpeech, _drawnSpeech;

    private RockPaperScissorsGame _rockPaperScissorsGame;

    private void Awake()
    {
        _rockPaperScissorsGame = _di.Instantiate<RockPaperScissorsGame>();
    }

    public override void StartGame(Character character)
    {
        base.StartGame(character);

        _rockPaperScissorsGame.OnCharacterWon += OnGameWin;
        _rockPaperScissorsGame.OnCharacterLost += OnGameLose;
        _rockPaperScissorsGame.OnDrawn += OnGameDrawn;

        ChoicePanel.Show("Давай сыграем в Камень, Ножницы, Бумага", new());
        _coroutineServise.WaitForSecondsAndInvoke(1f, _rockPaperScissorsGame.StartGame);        
    }    

    protected override void EndGame()
    {
        base.EndGame();

        _rockPaperScissorsGame.OnCharacterWon -= OnGameWin;
        _rockPaperScissorsGame.OnCharacterLost -= OnGameLose;
        _rockPaperScissorsGame.OnDrawn -= OnGameDrawn;
    }

    private void OnGameWin()
    {
        OnGameResult(_winSpeech);
    }
    private void OnGameLose()
    {
        OnGameResult(_loseSpeech);
    }
    private void OnGameDrawn()
    {
        OnGameResult(_drawnSpeech);
    }
}
