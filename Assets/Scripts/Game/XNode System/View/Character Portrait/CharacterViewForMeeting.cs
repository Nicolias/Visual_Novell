using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class CharacterViewForMeeting : MonoBehaviour, ICharacterView
{
    private Image _selfImage;
    private Button _selfButton;

    private CharacterType _characterType;
    private ICharacterPortraitModel _characterData;

    private Meeting _meeting;
    private IEnumerable<PastimeOnLocationType> _actionsVariation;

    public Image Image => _selfImage;
    public CharacterType Type => _characterType;
    public ICharacterPortraitModel Data => _characterData;
    public IEnumerable<PastimeOnLocationType> AvailablePastimes => _actionsVariation;
    public GameObject GameObject => gameObject;

    public void Initialize(ICharacterPortraitModel characterData, Meeting meeting)
    {
        _characterData = characterData;
        _characterType = characterData.CharacterType;
        _actionsVariation = characterData.Location.ActionsList;
        _meeting = meeting;
    }

    private void Awake()
    {
        _selfImage = GetComponent<Image>();
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveListener(OnClicked);
    }

    public void SetInteractable(bool isInteractable)
    {
        _selfButton.interactable = isInteractable;
    }

    private void OnClicked()
    {
        _meeting.Enter(this);
    }
}
