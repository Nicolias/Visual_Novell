using Characters;
using Game.GuessNumber;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GuessNumber : MiniGame
{
    [Inject] private DiContainer _di;

    [SerializeField] private string _winSpeech, _loseSpeech, _drawnSpeech;
    [SerializeField] private GuessNumberPanel _guessNumberPanel;

    private GuessNumberGame _guessNumberGame;

    private List<(int, int)> _boundsVariation = new()
    {
        new(1, 3),
        new(1, 10)
    };
    private string _currentWinSpeech;

    private void Awake()
    {
        _guessNumberGame = _di.Instantiate<GuessNumberGame>();
    }

    public override void StartGame(Character character)
    {
        base.StartGame(character);

        _guessNumberGame.OnCharacterWon += OnGameWin;
        _guessNumberGame.OnCharacterLost += OnGameLose;
        _guessNumberGame.OnDrawn += OnGameDrawn;

        ChoicePanel.Show("Давай сыграем в угадай число", GetChoiceElements(_boundsVariation));
    }

    protected override void EndGame()
    {
        base.EndGame();

        _guessNumberGame.OnCharacterWon -= OnGameWin;
        _guessNumberGame.OnCharacterLost -= OnGameLose;
        _guessNumberGame.OnDrawn -= OnGameDrawn;
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

    private void OnGameWin()
    {
        OnGameResult(_currentWinSpeech);
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
