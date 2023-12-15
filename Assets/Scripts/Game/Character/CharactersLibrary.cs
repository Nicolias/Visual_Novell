using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class CharactersLibrary : MonoBehaviour, ISaveLoadObject
{
    [Inject] private StaticData _staticData;
    [Inject] private SaveLoadServise _saveLoadServise;
    [Inject] private TimesOfDayServise _timesOfDayServise;
    
    [SerializeField] private List<CharacterSO> _allCharactersSO;

    private List<ICharacter> _characters;
    
    public IEnumerable<ICharacter> AllCharacters => _characters;

    private void Awake()
    {
        _characters = GenerateCharacters(_allCharactersSO);

        Add();
        Load();
    }

    public ICharacter GetCharacter(CharacterType characterType)
    {
        foreach (var character in _characters)
            if (character.Type == characterType)
                return character;

        throw new ArgumentOutOfRangeException();
    }

    public IEnumerable<ICharacter> GetCharacters(IEnumerable<CharacterSO> charactersSO)
    {
        List<ICharacter> characters = new List<ICharacter>();

        foreach (var characterSo in charactersSO)
            characters.Add(_characters.Find(character => character.Type == characterSo.Type));

        return characters;
    }

    public void AddPointsTo(CharacterType characterType, int pointsAmount)
    {
        var character = _characters.Find(character => character.Type == characterType);
        character.AccureSympathyPoints(pointsAmount);
    }

    public void DecreesPointsFrom(CharacterType characterType, int pointsAmount)
    {
        var character = _characters.Find(character => character.Type == characterType);
        character.DecreesSympathyPoints(pointsAmount);
    }

    private List<ICharacter> GenerateCharacters(List<CharacterSO> charactersSO)
    {
        List<ICharacter> characters = new List<ICharacter>();
        
        for (int i = 0; i < charactersSO.Count; i++)
            characters.Add(new Character(charactersSO[i], _staticData, _saveLoadServise, _timesOfDayServise, i));

        return characters;
    }

    public void Add()
    {
        _saveLoadServise.Add(this);
    }

    public void Save()
    {
        _characters.ForEach(character => character.Save());
    }

    public void Load()
    {
        _characters.ForEach(character => character.Load());
    }
}