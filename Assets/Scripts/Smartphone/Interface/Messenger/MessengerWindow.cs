using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MessengerWindow : MonoBehaviour
{
    [SerializeField] private Messenger _messenger;

    [SerializeField] private MessengerContact _contactTemplate;
    [SerializeField] private Transform _contactsContainer;

    [Inject] private DiContainer _di;

    private List<MessengerContact> _contactsView = new();

    private void Start()
    {
        foreach (var contact in _messenger.Contacts)
            CreateNewContact(contact);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void CreateNewContact(ContactElement ContactElement)
    {
        var newContact = _di.InstantiatePrefabForComponent<MessengerContact>(_contactTemplate, _contactsContainer);
        newContact.Initialize(ContactElement);
        _contactsView.Add(newContact);
    }
}
