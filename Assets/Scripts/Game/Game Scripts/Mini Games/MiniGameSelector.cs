using Characters;
using MiniGameNamespace;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class MiniGameSelector : MonoBehaviour, ICloseable
{
    [Inject] private readonly ChoicePanel _choicePanel;
    [Inject] private readonly CharactersLibrary _charactersLibrary;
    [Inject] private readonly Battery _battery;

    [SerializeField] private TMP_Text _sympathyCounter;
    [SerializeField] private TMP_Text _chargeCounter;
    [SerializeField] private List<MiniGame> _miniGames;
    [SerializeField] private int _startGameCost = 5;

    private Character _currentCharacter;

    public event Action Closed;

    private void OnEnable()
    {
        foreach (var miniGame in _miniGames)
            miniGame.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        foreach (var miniGame in _miniGames)
            miniGame.GameEnded -= OnGameEnded;
    }

    public void Enter(CharacterType character)
    {
        if (_battery.ChargeLevel < _startGameCost)
        {
            _choicePanel.Show("Недостаточно энергии", new List<ChoiseElement>()
            {
                new ChoiseElement("Принять", () => Close())
            });
            return;
        }

        gameObject.SetActive(true);

        _currentCharacter = _charactersLibrary.GetCharacter(character);
        _choicePanel.Show("Выбери игру", GetChoiceElements());

        UpdateSympathyView(_currentCharacter.SympathyPoints);
    }

    public void Close()
    {
        _choicePanel.Hide();
        gameObject.SetActive(false);
        Closed?.Invoke();
    }

    private List<ChoiseElement> GetChoiceElements()
    {
        List<ChoiseElement> choiseElements = new();

        foreach (var miniGame in _miniGames)
            choiseElements.Add(new ChoiseElement(miniGame.GameName, () => miniGame.StartGame(_currentCharacter)));

        choiseElements.Add(new ChoiseElement("Закончить игры.", Close));

        return choiseElements;
    }

    private void OnGameEnded()
    {
        _battery.Decreese(_startGameCost);
        UpdateSympathyView(_currentCharacter.SympathyPoints);

        Enter(_currentCharacter.Type);
    }

    private void UpdateSympathyView(int sympathyPoints)
    {
        _sympathyCounter.text = $"Симпатия: {sympathyPoints}";
        _chargeCounter.text = $"Осталось энергии: {_battery.ChargeLevel}%";
    }
}
