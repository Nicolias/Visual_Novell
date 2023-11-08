using Characters;
using Factory.Cells;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ContactsWindow : WindowInSmartphone
{
    [SerializeField] private Smartphone _smartphone;
    [SerializeField] private Button _closeButton;

    [SerializeField] private List<Location> _locationForMeeting;

    [SerializeField] private List<Character> _contacts = new List<Character>();
    [SerializeField] private Transform _characterCellsContainer;

    private Canvas _selfCanvas;
    private ChoicePanel _choicePanel;

    private List<Cell<Character>> _characterCells = new List<Cell<Character>>();

    [Inject]
    public void Construct(CellsFactoryCreater cellsFactoryCreater, IChoicePanelFactory choicePanelFactory)
    {
         _characterCells = cellsFactoryCreater.CreateCellsFactory<Character>().CreateCellsView(_contacts, _characterCellsContainer);
        _choicePanel = choicePanelFactory.CreateChoicePanel(transform);
    }

    private void Awake()
    {
        _selfCanvas = GetComponent<Canvas>();
        _closeButton.onClick.AddListener(Hide);
    }

    protected override void OnEnabled()
    {
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

        foreach (var locationForMeeting in _locationForMeeting)
        {
            ChoiseElement choiseElement = new ChoiseElement(locationForMeeting.Name, () => MoveTo(selectedCharacter, locationForMeeting));
            choiseElements.Add(choiseElement);
        }

        return choiseElements;
    }

    private void MoveTo(Character character, Location location)
    {
        location.Invite(character);
        _smartphone.Hide();
        Hide();
    }
}