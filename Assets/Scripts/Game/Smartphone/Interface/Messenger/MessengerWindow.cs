using Factory.Messenger;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MessengerWindow : MonoBehaviour
{
    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Button _closeButton;

    [Inject] private ContactFactory _contactFactory;

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

    public MessengerContact CreateContactView(ContactElement contactElement)
    {
        var contactView = _contactFactory.CreateNewContactView(contactElement);
        _contactsView.Add(contactElement, contactView);

        return contactView;
    }

    public MessengerContact GetExistContactView(ContactElement contactElement)
    {
        return _contactsView[contactElement];
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;

        foreach (var contact in _contactsView)
            contact.Value.Hide();
    }
}
