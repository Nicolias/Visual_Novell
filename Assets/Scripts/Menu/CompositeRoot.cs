using UnityEngine;
using Zenject;

public class CompositeRoot : MonoBehaviour
{
    [Inject] private SaveLoadServise _saveLoadServise;
    [Inject] private CharactersLibrary _charactersLibrary;
    [Inject] private StaticData _staticData;

    private void OnEnable()
    {
        _saveLoadServise.Initialized += OnInitialized;   
    }

    private void OnInitialized()
    {

    }
}
