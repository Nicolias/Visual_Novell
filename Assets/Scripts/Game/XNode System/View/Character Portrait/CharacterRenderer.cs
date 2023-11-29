using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using SaveData;

public class CharacterRenderer : MonoBehaviour, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private CharacterViewFactory _characterViewFactory;

    private Dictionary.Dictionary<CharacterPortraitPosition, Transform> _positions;
    private List<CharacterPortraitData> _charactersList = new List<CharacterPortraitData>();

    private const string _saveKey = "CharacterPortrait";

    public event Action Complite;

    private void Awake()
    {
         _positions = _characterViewFactory.Positions;
    }

    private void OnEnable()
    {
        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        Save();
    }

    public void Show(ICharacterPortraitModel character)
    {
        if (IsCharacterExist(character.CharacterType, out CharacterPortraitData characterData) == false)
        {
            CharacterPortraitData newCharacterView = _characterViewFactory.Create(character);
            _charactersList.Add(newCharacterView);

            DOTween.Sequence()
                .Append(newCharacterView.Image.DOColor(new Color(1, 1, 1, 0), 0))
                .Append(newCharacterView.Image.DOColor(new Color(1, 1, 1, 1), 0.5f))
                .AppendCallback(() => Complite?.Invoke())
                .Play();
        }
        else
        {
            DOTween.Sequence()
                .Append(characterData.Image.DOColor(new Color(1, 1, 1, 0), 0.2f))
                .AppendCallback(() => ChangePosition(characterData, character))
                .Append(characterData.Image.DOColor(new Color(1, 1, 1, 1), 0.2f))
                .AppendCallback(() => Complite?.Invoke())
                .Play();
        }
    }

    public void Delete(ICharacterPortraitModel character)
    {
        if (IsCharacterExist(character.CharacterType, out CharacterPortraitData existCharacter))
        {
            DOTween.Sequence()
                .Append(existCharacter.Image.DOColor(new Color(1, 1, 1, 0), 0.5f))
                .AppendCallback(() => Complite?.Invoke())
                .Play();

            _charactersList.Remove(existCharacter);
            Destroy(existCharacter.Image.gameObject);
        }
    }

    public void DeleteAllCharacters()
    {
        foreach (var character in _charactersList)
            Destroy(character.Image.gameObject);

        _charactersList.Clear();
    }

    private bool IsCharacterExist(CharacterType characterType, out CharacterPortraitData exist)
    {
        exist = null;

        foreach (var character in _charactersList.Where(character => character.CharacterType == characterType))
        {
            exist = character;
            return true;
        }

        return false;
    }

    private void ChangePosition(CharacterPortraitData characterData, ICharacterPortraitModel character)
    {
        characterData.Image.sprite = character.Sprite;
        characterData.Position = character.PositionType;

        if(_positions.TryGet(character.PositionType, out Transform transformForSpawn))
            characterData.Image.transform.SetParent(transformForSpawn);
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new IntData()
        {
            Int = _charactersList.Count
        });

        for (int i = 0; i < _charactersList.Count; i++)
                _saveLoadServise.Save($"{_saveKey}/{i}", _charactersList[i]);
    }

    public void Load()
    {
        int characterCount = _saveLoadServise.Load<IntData>(_saveKey).Int;

        for (int i = 0; i < characterCount; i++)
        {
            var characterData = _saveLoadServise.Load<CharacterPortraitData>($"{_saveKey}/{i}");

            CharacterModel characterModel = new CharacterModel(characterData);

            Show(characterModel);
        }
    }

    private class CharacterModel : ICharacterPortraitModel
    {
        public CharacterType CharacterType { get; private set; }
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }
        public CharacterPortraitPosition PositionType { get; private set; }

        public Vector2 PositionOffset { get; private set; }

        public Vector3 ScaleOffset { get; private set; }

        public LocationSO Location { get; private set;}

        public CharacterModel(CharacterPortraitData characterData)
        {
            CharacterType = characterData.CharacterType;
            Name = characterData.Name;
            Sprite = characterData.Sprite;
            PositionType = characterData.Position;
            PositionOffset = characterData.PositionOffset;
            ScaleOffset = characterData.ScaleOffset;

            Location = characterData.Location;
        }
    }
}