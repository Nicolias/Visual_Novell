using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharactersLibrary : MonoBehaviour
{
    [SerializeField] private List<Character> _allCharacters;
    [Inject] private StaticData _staticData;

    private void Awake()
    {
        List<Character> allCharacters = new();

        for (int i = 0; i < _allCharacters.Count; i++)
            allCharacters.Add(new Character(0,1, _staticData, _allCharacters[i].CharacterType));

        _allCharacters = allCharacters;
    }

    public void AddPointsTo(CharacterType characterType, int poinsAmount)
    {
        var character = _allCharacters.Find(x => x.CharacterType == characterType);
        character.AddPoints(poinsAmount);
    }
}
