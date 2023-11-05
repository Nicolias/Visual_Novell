using Characters;
using Factory.Cells;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ContactsWindow : MonoBehaviour
{
    private CharactersLibrary _charactersLibrary;

    [SerializeField] private List<Location> _locationForMeeting;

    [SerializeField] private List<Character> _contacts = new List<Character>();
    [SerializeField] private Transform _characterCellsContainer;

    private List<Cell<Character>> _characterCells = new List<Cell<Character>>();

    [Inject]
    public void Construct(CellsFactoryCreater cellsFactoryCreater)
    {
         _characterCells = cellsFactoryCreater.CreateCellsFactory<Character>().CreateCellsView(_contacts, _characterCellsContainer);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void MoveTo(Character character, Location location)
    {
        location.Invite(character);
    }
}