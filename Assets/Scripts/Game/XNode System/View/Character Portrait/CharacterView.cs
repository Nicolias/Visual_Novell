using StateMachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class CharacterView : MonoBehaviour, ICharacterView, IByStateMachineChangable
{
    private Image _selfImage;
    private Button _selfButton;

    private CharacterType _characterType;
    private ICharacterPortraitModel _characterData;

    private IEnumerable<PastimeOnLocationType> _actionsVariation;

    private IGameStateVisitor _gameStateVisitor;
    private Meeting _meeting;

    public Image Image => _selfImage;
    public CharacterType Type => _characterType;
    public ICharacterPortraitModel Data => _characterData;
    public IEnumerable<PastimeOnLocationType> AvailablePastimes => _actionsVariation;
    public GameObject GameObject => gameObject;

    private void OnValidate()
    {
        enabled = false;
    }

    public void Initialize(ICharacterPortraitModel characterData, Meeting meeting, GameStateMachine gameStateMachine)
    {
        _characterData = characterData;
        _characterType = characterData.CharacterType;
        _meeting = meeting;

        if(characterData.Location != null)
            _actionsVariation = characterData.Location.ActionsList;

        _gameStateVisitor = new GameStateVisitor(gameStateMachine, this);
        enabled = true;
    }

    private void Awake()
    {
        _selfImage = GetComponent<Image>();
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(OnClicked);
        _gameStateVisitor.SubscribeOnGameStateMachine();
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveListener(OnClicked);
        _gameStateVisitor.UnsubsciribeFromGameStateMachine();
    }

    public void SetInteractable(bool isInteractable)
    {
        _selfButton.interactable = isInteractable;
    }

    void IByStateMachineChangable.ChangeBehaviourBy(PlayState playState)
    {
        SetInteractable(true);
    }

    void IByStateMachineChangable.ChangeBehaviourBy(StoryState storyState)
    {
        SetInteractable(false);
    }

    private void OnClicked()
    {
        _meeting.Enter(this);
    }
}