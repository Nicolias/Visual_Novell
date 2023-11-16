using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharactersLibrary : MonoBehaviour
{
    [SerializeField] private List<Character> _allCharacters;

    public IEnumerable<Character> AllCharacters => _allCharacters;

    public Character GetCharacter(CharacterType characterType)
    {
        foreach (var character in _allCharacters)
            if (character.Type == characterType)
                return character;

        throw new ArgumentOutOfRangeException();
    }

    public void AddPointsTo(CharacterType characterType, int pointsAmount)
    {
        var character = _allCharacters.Find(x => x.Type == characterType);
        character.AccureSympathyPoints(pointsAmount);
    }

    public void DecreesPointsFrom(CharacterType characterType, int pointsAmount)
    {
        var character = _allCharacters.Find(x => x.Type == characterType);
        character.DecreesSympathyPoints(pointsAmount);
    }

    //public void Save()
    //{
    //    _saveLoadServise.Save(_saveKey, new SaveData.IntData() { Int = _allCharacters.Count });

    //    for (int i = 0; i < _allCharacters.Count; i++)
    //    {
    //        _saveLoadServise.Save($"{_saveKey}/{i}", new SaveData.CharacterData()
    //        {
    //            CharacterType = _allCharacters[i].CharacterType,
    //            SympathyLevel = _allCharacters[i].SympathyLevel,
    //            SympathyPoints = _allCharacters[i].SympathyPoints
    //        });
    //    }
    //}

    //public void Load()
    //{
    //    int characterCount = _saveLoadServise.Load<SaveData.IntData>(_saveKey).Int;

    //    for (int i = 0; i < characterCount; i++)
    //    {
    //        var characterData = _saveLoadServise.Load<SaveData.CharacterData>($"{_saveKey}/{i}");
    //        Character character = new(characterData.SympathyPoints, characterData.SympathyLevel, 
    //            _staticData, characterData.CharacterType);

    //        _allCharacters.Add(character);
    //    }
    //}
}