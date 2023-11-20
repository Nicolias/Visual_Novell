using Characters;
using Factory.Cells;
using System.Collections.Generic;
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

    [SerializeField] private List<Character> _contacts = new List<Character>();
    [SerializeField] private Transform _characterCellsContainer;

    private Canvas _selfCanvas;
    private ChoicePanel _choicePanel;

    private LocationsManager _locationsManager;

    private List<Cell<Character>> _characterCells = new List<Cell<Character>>();

    [Inject]
    public void Construct(LocationsManager locationsManager, CellsFactoryCreater cellsFactoryCreater, IChoicePanelFactory choicePanelFactory)
    {
         _characterCells = cellsFactoryCreater.CreateCellsFactory<Character>().CreateCellsView(_contacts, _characterCellsContainer);
        _choicePanel = choicePanelFactory.CreateChoicePanel(transform);

        _locationsManager = locationsManager;
    }

    private void Awake()
    {
        _selfCanvas = GetComponent<Canvas>();
        _closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        ChangeEnableInSmartphone(true);
        _smartphone.Save();
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

    private void OnCharacterCellClicked(Character character)
    {
        _choicePanel.Show("Выберите локацию для приглашения", CreateLocationButtons(character));
    }

    private List<ChoiseElement> CreateLocationButtons(Character selectedCharacter)
    {
        List<ChoiseElement> choiseElements = new List<ChoiseElement>();

        foreach (var locationForMeeting in _locationsManager.AvailableLocations)
        {
            ChoiseElement choiseElement = new ChoiseElement(locationForMeeting.Name, () => MoveTo(selectedCharacter, locationForMeeting));
            choiseElements.Add(choiseElement);
        }

        return choiseElements;
    }

    private void MoveTo(Character character, Location location)
    {
        _map.ChangeLocation(location);
        _charactersRenderer.Show(location.Get(character));

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