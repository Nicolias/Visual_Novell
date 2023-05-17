using System;
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

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void CreateNewContactView(ContactElement ContactElement)
    {
        var newContact = _di.InstantiatePrefabForComponent<MessengerContact>(_contactTemplate, _contactsContainer);
        newContact.Initialize(ContactElement);
        _contactsView.Add(newContact);
    }
}
