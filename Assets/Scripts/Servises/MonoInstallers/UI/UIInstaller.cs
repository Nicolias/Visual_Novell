using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private ChatView _chatView;

    public override void InstallBindings()
    {
        Container.
            Bind<ChatView>().
            FromInstance(_chatView).
            AsSingle();
    }
}