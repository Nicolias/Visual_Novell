using Factory.Messenger;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MessengerWindow : MonoBehaviour, IMessengerWindow
{
    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Button _closeButton;

    [Inject] private ContactFactory _contactFactory;
    [Inject] private ChatFactory _chatFactory;

    private List<ContactElement> _contacts = new();
    private Dictionary<ContactElement, MessengerContact> _contactsView = new();

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        _selfCanvas.enabled = true;
    }

    public Chat CreateChat(MessegeData newMessege)
    {
        ContactElement existContact = _contacts.Find(x => x.Name == newMessege.ContactName);
        MessengerContact currentContactView;

        if (existContact == null)
        {
            ContactElement newConatactData = new(newMessege.ContactName);
            _contacts.Add(newConatactData);

            currentContactView = CreateContactView(newConatactData);
        }
        else
        {
            currentContactView = GetExistContactView(existContact);
        }

        var chat = _chatFactory.Create(newMessege.Messege, currentContactView.ChatsContainer);

        return chat;
    }

    private MessengerContact CreateContactView(ContactElement contactElement)
    {
        var contactView = _contactFactory.CreateNewContactView(contactElement);
        _contactsView.Add(contactElement, contactView);

        return contactView;
    }

    private MessengerContact GetExistContactView(ContactElement contactElement)
    {
        if (_contactsView.ContainsKey(contactElement) == false)
            throw new ArgumentOutOfRangeException("Такого контакта не существует.");

        return _contactsView[contactElement];
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;

        foreach (var contact in _contactsView)
            contact.Value.Hide();
    }
}