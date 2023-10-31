using Characters;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class MiniGameSelector : MonoBehaviour
{
    public event Action OnGameClosed;
    public event Action OnGameEnded;

    [Inject] private readonly ChoicePanel _choicePanel;
    [Inject] private readonly CharactersLibrary _charactersLibrary;
    [Inject] private readonly Battery _battery;

    [SerializeField] private TMP_Text _sympathyCounter;
    [SerializeField] private TMP_Text _chargeCounter;
    [SerializeField] private List<MiniGame<AbstractMiniGame>> _miniGames;
    [SerializeField] private int _startGameCost;

    private Character _currentCharacter;

    private void OnEnable()
    {
        foreach (var miniGame in _miniGames)
        {
            miniGame.GameEnded += OnGameEnd;
            miniGame.GameClosed += CloseMiniGamesSelection;
            miniGame.GameRestarted += OnGameRestart;
        }            
    }

    private void OnDisable()
    {
        foreach (var miniGame in _miniGames)
        {
            miniGame.GameEnded -= OnGameEnd;
            miniGame.GameClosed -= CloseMiniGamesSelection;
            miniGame.GameRestarted -= OnGameRestart;
        }
    }

    public void ShowMiniGameSelectoin(CharacterType character)
    {
        if (_battery.ChargeLevel < _startGameCost)
        {
            _choicePanel.Show("Недостаточно энергии", new()
            {
                new("Принять", () => CloseMiniGamesSelection())
            });
            return;
        }

        gameObject.SetActive(true);

        _currentCharacter = _charactersLibrary.GetCharacter(character);
        _choicePanel.Show("Выбери игру", GetChoiceElements());

        UpdateSympathyView(_currentCharacter.SympathyPoints);
    }

    public void CloseMiniGamesSelection()
    {
        _choicePanel.Hide();
        gameObject.SetActive(false);
    }

    private List<ChoiseElement> GetChoiceElements()
    {
        List<ChoiseElement> choiseElements = new();

        foreach (var miniGame in _miniGames)
            choiseElements.Add(new(miniGame.GameName, () => miniGame.StartGame(_currentCharacter)));

        return choiseElements;
    }

    private void OnGameEnd()
    {
        _battery.Decreese(_startGameCost);
        OnGameEnded?.Invoke();
        UpdateSympathyView(_currentCharacter.SympathyPoints);
    }
    private void OnGameRestart()
    {
        ShowMiniGameSelectoin(_currentCharacter.Type);
    }

    private void UpdateSympathyView(int sympathyPoints)
    {
        _sympathyCounter.text = $"Симпатия: {sympathyPoints}";
        _chargeCounter.text = $"Осталось энергии: {_battery.ChargeLevel}%";
    }
}
