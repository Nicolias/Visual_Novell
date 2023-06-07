using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class MiniGame : MonoBehaviour
{
    public event Action OnGameEnded;
    public event Action OnGameRestarted;
    public event Action OnGameClosed;

    [Inject] protected readonly ChoicePanel ChoicePanel;
    [Inject] protected readonly Battery Battery;   

    [field: SerializeField] public string GameName { get; private set; }

    protected Character CurrentCharacter { get; private set; }

    public virtual void StartGame(Character character)
    {
        CurrentCharacter = character;
    }

    protected virtual void EndGame()
    {
        OnGameEnded?.Invoke();
    }

    protected virtual void OnGameResult(string resultCharacterSpeech)
    {
        CurrentCharacter.AccureSympathyPoints(1);

        ChoicePanel.Show(resultCharacterSpeech, new List<ChoiseElement>()
        {
            new("Повторить", () => 
            {
                OnGameRestarted?.Invoke();
                EndGame();
            })
            //new("Закончить", () => 
            //{
            //    OnGameClosed?.Invoke();
            //    OnGameEnded?.Invoke();
            //})
        });
    }
}