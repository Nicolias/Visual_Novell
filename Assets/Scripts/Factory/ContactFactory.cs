using UnityEngine;
using Zenject;

namespace Factory.Messenger
{
    public class ContactFactory : MonoBehaviour
    {
        [Inject] private DiContainer _di;

        [SerializeField] private Transform _contactsContainer;
        [SerializeField] private MessengerContact _contactTemplate;

        public MessengerContact CreateNewContactView(ContactElement ContactElement)
        {
            var newContact = _di.InstantiatePrefabForComponent<MessengerContact>(_contactTemplate, _contactsContainer);
            newContact.Initialize(ContactElement);
            return newContact;
        }
    }
}