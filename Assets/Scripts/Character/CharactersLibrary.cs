using Characters;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class CharactersLibrary : MonoBehaviour
{
    [Inject] private StaticData _staticData;
    [SerializeField] private List<Character> _allCharacters;

    public List<Character> AllCharacters => _allCharacters;

    private void Awake()
    {
        List<Character> allCharacters = new();

        for (int i = 0; i < _allCharacters.Count; i++)
            allCharacters.Add(new Character(0, 1, _staticData, _allCharacters[i].CharacterType));

        _allCharacters = allCharacters;
    }

    public void AddPointsTo(CharacterType characterType, int pointsAmount)
    {
        var character = _allCharacters.Find(x => x.CharacterType == characterType);
        character.AccureSympathyPoints(pointsAmount);
    }

    public void DecreesPointsFrom(CharacterType characterType, int pointsAmount)
    {
        var character = _allCharacters.Find(x => x.CharacterType == characterType);
        character.DecreesSympathyPoints(pointsAmount);
    }
}