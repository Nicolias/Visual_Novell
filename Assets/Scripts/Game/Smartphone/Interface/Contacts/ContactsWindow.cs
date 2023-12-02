using Characters;
using Factory.Cells;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ContactsWindow : WindowInSmartphone
{
    [SerializeField] private Map _map;
    [SerializeField] private CharacterRenderer _charactersRenderer;

    [SerializeField] private Meeting _meeting;
    [SerializeField] private Smartphone _smartphone;
    [SerializeField] private Button _closeButton;

    [SerializeField] private List<LocationSO> _locationsSOForMeeting = new List<LocationSO>();
    [SerializeField] private List<CharacterSO> _contacts = new List<CharacterSO>();

    [SerializeField] private Transform _characterCellsContainer;

    private Battery _battery;

    private int _startMeetingCost = 10;
    private int _meetingSympathyBonus = 1;

    private Canvas _selfCanvas;
    private ChoicePanel _choicePanel;

    private List<ILocation> _locations;
    private List<Cell<ICharacter>> _characterCells = new List<Cell<ICharacter>>();

    [Inject]
    public void Construct(CharactersLibrary charactersLibrary, LocationsManager locationsManager, CellsFactoryCreater cellsFactoryCreater, 
        IChoicePanelFactory choicePanelFactory, Battery battery)
    {
        _characterCells = cellsFactoryCreater.CreateCellsFactory<ICharacter>().CreateCellsView(charactersLibrary.GetCharacters(_contacts), _characterCellsContainer);
        _choicePanel = choicePanelFactory.CreateChoicePanel(transform);

        _locations = locationsManager.Get(_locationsSOForMeeting).ToList();

        _battery = battery;
    }

    private void Awake()
    {
        _selfCanvas = GetComponent<Canvas>();
        _closeButton.onClick.AddListener(Hide);
    }

    protected override void OnDisabled()
    {
        _closeButton.onClick.RemoveListener(Hide);
    }

    protected override void OnOpenButtonClicked()
    {
        Show();
    }

    private void Show()
    {
        foreach (var characterCell in _characterCells)
            characterCell.Clicked += OnCharacterCellClicked;

        _selfCanvas.enabled = true;
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;

        foreach (var characterCell in _characterCells)
            characterCell.Clicked -= OnCharacterCellClicked;

        _choicePanel.Hide();
    }

    private void OnCharacterCellClicked(ICharacter character)
    {
        _choicePanel.Show("Выберите локацию для приглашения", CreateLocationsButtons(character));
    }

    private List<ChoiseElement> CreateLocationsButtons(ICharacter selectedCharacter)
    {
        List<ChoiseElement> choiseElements = new List<ChoiseElement>();

        foreach (var locationForMeeting in _locations)
        {
            if (locationForMeeting.IsAvailable)
            {
                ChoiseElement choiseElement = new ChoiseElement(locationForMeeting.Name, () => TryMoveTo(selectedCharacter, locationForMeeting));
                choiseElements.Add(choiseElement);
            }
        }

        return choiseElements;
    }

    private void TryMoveTo(ICharacter character, ILocation location)
    {
        if (_battery.CurrentValue < _startMeetingCost)
        {
            _choicePanel.Show("Недостаточно энергии", new List<ChoiseElement>()
            {
                new ChoiseElement("Принять", null)
            });
            return;
        }

        character.Invite(location.Data, _meetingSympathyBonus);
        _battery.Decreese(_startMeetingCost);

        _map.ChangeLocation(location);
        _charactersRenderer.Show(location.Data.Get(character.ScriptableObject));

        Hide();
        _smartphone.Hide();

        ChangeEnableInSmartphone(false);
        _meeting.Ended += OnMeetingEnded;
    }
 
    private void OnMeetingEnded()
    {
        _meeting.Ended -= OnMeetingEnded;
        ChangeEnableInSmartphone(true);
    }

    private void ChangeEnableInSmartphone(bool isEnabled)
    {
        Dictionary.Dictionary<SmartphoneWindows, bool> appsForChangeEnable = new Dictionary.Dictionary<SmartphoneWindows, bool>();

        appsForChangeEnable.Add(SmartphoneWindows.Map, isEnabled);
        appsForChangeEnable.Add(SmartphoneWindows.DUX, isEnabled);
        appsForChangeEnable.Add(SmartphoneWindows.Contacts, isEnabled);

        _smartphone.ChangeEnabled(appsForChangeEnable);
    }
}