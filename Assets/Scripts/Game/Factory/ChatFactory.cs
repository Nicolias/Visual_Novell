using UnityEngine;
using XNode;
using Zenject;

namespace Factory.Messenger
{
    public class ChatFactory : MonoBehaviour
    {
        [Inject] private DiContainer _di;

        [SerializeField] private Chat _chatButtonTemplate;

        public virtual Chat Create(NodeGraph newMessege, Transform chatsContainer)
        {
            var newChat = _di.InstantiatePrefabForComponent<Chat>(_chatButtonTemplate, chatsContainer);
            newChat.Initialize(newMessege);

            return newChat;
        }
    }
}
