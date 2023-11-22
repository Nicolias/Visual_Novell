using System;
using Factory.Artifacts;
using UnityEngine;
using Zenject;
using StateMachine;

public class ArtifactsManager : MonoBehaviour, ISaveLoadObject, IByStateMachineChangable
{
    [SerializeField] private Artifact _artifactData;

    [SerializeField] private Battery _battery;
    [SerializeField] private Wallet _wallet;

    private IGameStateVisitor _gameStateVisitor;
    private ArtifactsFactory _artifactsFactory;
    private TimesOfDayServise _timesOfDayServise;
    private SaveLoadServise _saveLoadServise;

    private DateTime _nextTimeForResetCollection = new DateTime();

    private int _moneyReward = 20;
    private int _enargyReward = 20;

    private bool _isArtifactsCreated;

    private string _saveKey = "ArtifactsManager";

    [Inject]
    public void Construct(LocationsManager locationsManager, SaveLoadServise saveLoadServise, 
        CollectionPanel collectionPanel, TimesOfDayServise timesOfDayServise, GameStateMachine gameStateMachine)
    {
        _artifactsFactory = new ArtifactsFactory(locationsManager, saveLoadServise, collectionPanel, _artifactData);
        _timesOfDayServise = timesOfDayServise;
        _saveLoadServise = saveLoadServise;

        _gameStateVisitor = new GameStateVisitor(gameStateMachine, this);
    }

    private void Awake()
    {
        if (_saveLoadServise.HasSave(_saveKey))
            Load();

        _gameStateVisitor.RecognizeCurrentGameState();
    }

    private void OnEnable()
    {
        _artifactsFactory.AllArtifactsCollected += AccureRewards;
        _gameStateVisitor.SubscribeOnGameStateMachine();
    }

    private void OnDisable()
    {
        _gameStateVisitor.UnsubsciribeFromGameStateMachine();
        _artifactsFactory.AllArtifactsCollected -= AccureRewards;
        _artifactsFactory.Dispose();
        Save();
    }

    public void TryShowArtifacts()
    {
        if (_timesOfDayServise.CurrentTime >= _nextTimeForResetCollection)
        {
            DateTime currentTime = _timesOfDayServise.CurrentTime;
            _nextTimeForResetCollection = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day + 1, 1, 0, 0);

            _artifactsFactory.ResetCollection();
        }
    }

    private void AccureRewards()
    {
        _wallet.Accure(_moneyReward);
        _battery.Accure(_enargyReward);

        _isArtifactsCreated = false;
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.DateTimeData()
        {
            Year = _nextTimeForResetCollection.Year,
            Month = _nextTimeForResetCollection.Month,
            Day = _nextTimeForResetCollection.Day
        });
        _saveLoadServise.Save(_saveKey + "1", new SaveData.BoolData() { Bool = _isArtifactsCreated });
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.DateTimeData>(_saveKey);
        _nextTimeForResetCollection = new DateTime(data.Year, data.Month, data.Day);

        _isArtifactsCreated = _saveLoadServise.Load<SaveData.BoolData>(_saveKey + "1").Bool;
    }

    void IByStateMachineChangable.ChangeBehaviourBy(PlayState playState)
    {        
        TryShowArtifacts();
    }

    void IByStateMachineChangable.ChangeBehaviourBy(StoryState storyState)
    {

    }
}