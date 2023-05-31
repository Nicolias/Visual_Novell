using Characters;
using Game.RockPaperScissors;
using System;
using UnityEngine;
using Zenject;

public class RockPaperScissors : MiniGame
{
    [Inject] private DiContainer _di;

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
        
        _rockPaperScissorsGame.StartGame();
    }    

    protected override void EndGame()
    {
        base.EndGame();

        _rockPaperScissorsGame.OnCharacterWon -= OnGameWin;
        _rockPaperScissorsGame.OnCharacterLost -= OnGameLose;
        _rockPaperScissorsGame.OnDrawn -= OnGameDrawn;
    }

    protected override void OnGameResult(string resultCharacterSpeech)
    {
        CurrentCharacter.AccureSympathyPoints(1);
        
        base.OnGameResult(resultCharacterSpeech);
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
