using UnityEngine;
using Zenject;

namespace Factory.Messenger
{
    public class ContactFactory : MonoBehaviour
    {
        [Inject] private DiContainer _di;

        [SerializeField] private Transform _contactsContainer;
        [SerializeField] private ContactViewInMessenger _contactTemplate;

        public virtual ContactViewInMessenger CreateNewContactView(ContactData ContactElement)
        {
            var newContact = _di.InstantiatePrefabForComponent<ContactViewInMessenger>(_contactTemplate, _contactsContainer);
            newContact.Initialize(ContactElement);
            return newContact;
        }
    }
}