using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class MiniGame<T> : MonoBehaviour where T : AbstractMiniGame
{
    [Inject] private DiContainer _di;
    [Inject] protected readonly ChoicePanel ChoicePanel;
    [Inject] protected readonly Battery Battery;

    [field: SerializeField] public string GameName { get; private set; }

    protected Character CurrentCharacter { get; private set; }

    protected T Game { get; private set; }
    protected abstract string WinSpeech { get; }
    protected abstract string LoseSpeech { get; }
    protected abstract string DrawnSpeech { get; }

    public event Action GameEnded;
    public event Action GameRestarted;
    public event Action GameClosed;

    private void Awake()
    {
        Game = _di.Instantiate<T>();
    }

    public void StartGame(Character character)
    {
        CurrentCharacter = character;

        Game.CharacterWon += OnGameWin;
        Game.CharacterLost += OnGameLose;
        Game.Drawn += OnGameDrawn;
    }

    protected abstract void SetUpGame();

    private void EndGame()
    {
        GameEnded?.Invoke();

        Game.CharacterWon -= OnGameWin;
        Game.CharacterLost -= OnGameLose;
        Game.Drawn -= OnGameDrawn;
    }

    private void OnGameResult(string resultCharacterSpeech)
    {
        CurrentCharacter.AccureSympathyPoints(1);

        ChoicePanel.Show(resultCharacterSpeech, new List<ChoiseElement>()
        {
            new("Повторить", () => 
            {
                GameRestarted?.Invoke();
                EndGame();
            })
            //new("Закончить", () => 
            //{
            //    OnGameClosed?.Invoke();
            //    OnGameEnded?.Invoke();
            //})
        });
    }

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
}