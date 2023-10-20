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

    private ContactFactory _contactFactory;
    private ChatFactory _chatFactory;

    private List<ContactData> _contacts = new();
    private Dictionary<ContactData, ContactViewInMessenger> _contactsView = new();

    public event Action<ContactViewInMessenger> NewContactViewCreated;

    [Inject]
    public void Construct(ContactFactory contactFactory, ChatFactory chatFactory)
    {
        _contactFactory = contactFactory;
        _chatFactory = chatFactory;
    }

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
        if (TryGetContactView(newMessege.ContactName, out ContactViewInMessenger contactView) == false)
            contactView = CreateContactView(newMessege.ContactName);

        Chat chat = _chatFactory.Create(newMessege.Messege, contactView.ChatsContainer);
        contactView.Add(chat);

        return chat;
    }

    public bool TryGetContactView(string contactName, out ContactViewInMessenger contactView)
    {
        bool isContactExist = _contacts.Exists(contactElement => contactElement.Name == contactName);
        contactView = null;

        if (isContactExist)
        {
            ContactData contactElement = _contacts.Find(contactElement => contactElement.Name == contactName);

            if (contactElement == null)
                throw new InvalidOperationException("Не существует контакта с таким именем.");

            contactView = _contactsView[contactElement];
        }

        return isContactExist;
    }

    private ContactViewInMessenger CreateContactView(string contactName)
    {
        ContactData newConatactData = new(contactName);
        _contacts.Add(newConatactData);

        ContactViewInMessenger contactView = _contactFactory.CreateNewContactView(newConatactData);
        _contactsView.Add(newConatactData, contactView);

        NewContactViewCreated?.Invoke(contactView);

        return contactView;
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;

        foreach (var contact in _contactsView)
            contact.Value.Hide();
    }
}