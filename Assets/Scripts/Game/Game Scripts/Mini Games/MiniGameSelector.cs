using Characters;
using MiniGameNamespace;
using StateMachine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class MiniGameSelector : MonoBehaviour, ICloseable, IByStateMachineChangable
{
    [Inject] private readonly ChoicePanel _choicePanel;
    [Inject] private readonly CharactersLibrary _charactersLibrary;
    [Inject] private readonly Battery _battery;
    [Inject] private readonly GameStateMachine _gameStateMachine;

    [SerializeField] private TMP_Text _sympathyCounter;
    [SerializeField] private TMP_Text _chargeCounter;
    [SerializeField] private List<MiniGame> _miniGames;

    private int _startGameBatteryCost = 5;

    private GameStateVisitor _gameStateVisitor;

    private ICharacter _currentCharacter;

    private List<ChoiseElement> _gameChoises;

    public event Action Closed;
    public event Action GameEnded;

    private void Awake()
    {
        _gameStateVisitor = new GameStateVisitor(_gameStateMachine, this);
        _gameStateVisitor.RecognizeCurrentGameState();
        _gameStateVisitor.SubscribeOnGameStateMachine();
    }

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

    private void OnDestroy()
    {
        _gameStateVisitor.UnsubsciribeFromGameStateMachine();
    }

    void IByStateMachineChangable.ChangeBehaviourBy(PlayState playState)
    {
        _gameChoises = GetChoiceElements();
        _gameChoises.Add(new ChoiseElement("Закончить игры.", Close));
    }

    void IByStateMachineChangable.ChangeBehaviourBy(StoryState storyState)
    {
        _gameChoises = GetChoiceElements();
    }

    public void Enter(CharacterType character)
    {
        if (_battery.CurrentValue < _startGameBatteryCost)
        {
            _choicePanel.Show("Недостаточно энергии", new List<ChoiseElement>()
            {
                new ChoiseElement("Принять", () => Close())
            });
            return;
        }

        gameObject.SetActive(true);

        _currentCharacter = _charactersLibrary.GetCharacter(character);
        _choicePanel.Show("Выбери игру", _gameChoises);

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
        List<ChoiseElement> choiseElements = new List<ChoiseElement>();

        foreach (var miniGame in _miniGames)
            choiseElements.Add(new ChoiseElement(miniGame.GameName, () => miniGame.StartGame(_currentCharacter)));

        return choiseElements;
    }

    private void OnGameEnded()
    {
        _battery.Decreese(_startGameBatteryCost);
        UpdateSympathyView(_currentCharacter.SympathyPoints);

        Enter(_currentCharacter.Type);
        GameEnded?.Invoke();
    }

    private void UpdateSympathyView(int sympathyPoints)
    {
        _sympathyCounter.text = $"Симпатия: {sympathyPoints}";
        _chargeCounter.text = $"Осталось энергии: {_battery.CurrentValue}%";
    }
}
