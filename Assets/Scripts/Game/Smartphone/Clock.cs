using StateMachine;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class Clock : MonoBehaviour, IByStateMachineChangable, ISaveLoadObject
{
    [SerializeField] private TMP_Text _clockText;

    private DateTime _currentTime = new(1, 1, 1, 23, 34, 0);

    private GameStateVisitor _gameStateVisitor;
    private SaveLoadServise _saveLoadServise;
    private TimesOfDayServise _timesOfDayServise;

    private const string _saveKey = "Clock";

    [Inject]
    public void Construct(GameStateMachine gameStateMachine, TimesOfDayServise timesOfDayServise, SaveLoadServise saveLoadServise)
    {
        _gameStateVisitor = new GameStateVisitor(gameStateMachine, this);
        _timesOfDayServise = timesOfDayServise;
        _saveLoadServise = saveLoadServise;
    }

    private void OnEnable()
    {
        _gameStateVisitor.SubscribeOnGameStateMachine();
    }

    private void OnDisable()
    {
        _gameStateVisitor.UnsubsciribeFromGameStateMachine();
    }

    private void Start()
    {
        _saveLoadServise.Add(this);

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
        else
            SetTime(_currentTime.Hour, _currentTime.Minute);

        _gameStateVisitor.RecognizeCurrentGameState();
    }

    public void SetTime(int hour, int minute)
    {
        _currentTime = DateTime.MinValue;
        _currentTime = _currentTime.AddHours(hour);
        _currentTime = _currentTime.AddMinutes(minute);

        _clockText.text = $"{_currentTime:HH}:{_currentTime:mm}";
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.SmartphoneData()
        {
            Hors = _currentTime.Hour,
            Minuts = _currentTime.Minute,
        });
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.SmartphoneData>(_saveKey);

        SetTime(data.Hors, data.Minuts);
    }

    public void ChangeBehaviourBy(PlayState playState)
    {
        SetTime(_timesOfDayServise.CurrentTime.Hour, _timesOfDayServise.CurrentTime.Minute);

        _timesOfDayServise.TimeChanged += SetTime;
    }

    public void ChangeBehaviourBy(StoryState storyState)
    {
        _timesOfDayServise.TimeChanged -= SetTime;
    }

    public void Add()
    {
        _saveLoadServise.Add(this);
    }
}