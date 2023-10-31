using Characters;
using System.Collections.Generic;
using UnityEngine;

public class ContactsWindow : MonoBehaviour
{
    private CharactersLibrary _charactersLibrary;

    [SerializeField] private Location _location;

    [SerializeField] private List<Character> _contacts = new List<Character>();

    private void OnEnable()
    {
        _location.Invite(_contacts[0]);
    }

    public void MoveTo(Character character, Location location)
    {
        location.Invite(character);
    }
}