using Characters;
using Game.RockPaperScissors;
using System;
using UnityEngine;
using Zenject;

public class RockPaperScissors : MonoBehaviour
{
    public event Action OnGameEnded;

    [Inject] private CharactersLibrary _charactersLibrary;
    [Inject] private ChoisePanel _choisePanel;
    [Inject] private Battery _battery;
    [Inject] private DiContainer _di;

    [SerializeField] private int _startGameCost;

    private RockPaperScissorsGame _rockPaperScissorsGame;
    private Character _currentCharacter;

    private void Awake()
    {
        _rockPaperScissorsGame = _di.Instantiate<RockPaperScissorsGame>();
    }

    public bool StartGame(CharacterType characterType)
    {
        if (_battery.ChargeLevel <= _startGameCost)
            return false;

        _currentCharacter = _charactersLibrary.GetCharacter(characterType);

        _rockPaperScissorsGame.OnCharacterWon += OnGameResult;
        _rockPaperScissorsGame.OnCharacterLost += OnGameResult;
        _rockPaperScissorsGame.OnDrawn += OnGameResult;

        _battery.Decreese(_startGameCost);
        _rockPaperScissorsGame.StartGame();

        return true;
    }

    public void EndGame()
    {
        _rockPaperScissorsGame.OnCharacterWon -= OnGameResult;
        _rockPaperScissorsGame.OnCharacterLost -= OnGameResult;
        _rockPaperScissorsGame.OnDrawn -= OnGameResult;

        _choisePanel.Hide();

        OnGameEnded?.Invoke();
    }

    private void OnGameResult()
    {
        EndGame();
        _currentCharacter.AccureSympathyPoints(1);
    }
}