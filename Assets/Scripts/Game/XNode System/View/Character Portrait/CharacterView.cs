using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class CharacterView : MonoBehaviour
{
    private Image _selfImage;
    private Button _selfButton;
    private CharacterType _characterType;

    private Meeting _meeting;
    private IEnumerable<PastimeOnLocationType> _actionsVariation;

    public Image Image => _selfImage;
    public CharacterType Type => _characterType;
    public IEnumerable<PastimeOnLocationType> AvailablePastimes => _actionsVariation;

    public void Initialize(CharacterType character, Meeting meeting, Location location)
    {
        _characterType = character;
        _meeting = meeting;
        _actionsVariation = location.ActionsList;
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