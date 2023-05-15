using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messenger : MonoBehaviour
{
    public event Action<ContactElement> OnNewContactRecived;
    public event Action<MessegeElement> OnNewMessegeRecived;

    [SerializeField] private MessengerWindow _messengerWindow;
    [SerializeField] private Button _openMesengerButton;

    private List<ContactElement> _contacts = new();
    public IEnumerable<ContactElement> Contacts => _contacts;

    private void OnEnable()
    {
        _openMesengerButton.onClick.AddListener(() => _messengerWindow.Open());
    }

    private void OnDisable()
    {
        _openMesengerButton.onClick.RemoveAllListeners();
    }

    public void AddNewMessege(MessegeElement newMessege)
    {
        var existContact = _contacts.Find(x => x.Name == newMessege.ContactName);

        if (existContact != null)
        {
            existContact.AddMessege(newMessege);
        }
        else
        {
            var newConatact = new ContactElement(newMessege.ContactName, new() { newMessege.Messege });
            _contacts.Add(newConatact);

            _messengerWindow.CreateNewContact(newConatact);
        }            
    }
}
