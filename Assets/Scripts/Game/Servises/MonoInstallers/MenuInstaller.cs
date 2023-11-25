using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [Inject] private SaveLoadServise _saveLoadServise;

    public override void InstallBindings()
    {
        _saveLoadServise.ClearAllObjects();
    }
}