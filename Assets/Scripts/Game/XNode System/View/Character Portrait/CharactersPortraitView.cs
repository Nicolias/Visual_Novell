using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CharactersPortraitView : MonoBehaviour, ICharacterPortraitView, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private CharacterViewFactory _characterViewFactory;

    private List<Transform> _positions = new List<Transform>();
    private List<CharacterPortraitData> _charactersList = new List<CharacterPortraitData>();

    private const string _saveKey = "CharacterPortrait";

    public event Action Complite;

    private void Awake()
    {
        foreach (var position in _characterViewFactory.Positions)
            _positions.Add(position);
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
        characterData.SetPosition(character.PositionType);
        characterData.Image.transform.SetParent(_positions[(int)characterData.Position]);
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.IntData()
        {
            Int = _charactersList.Count
        });

        for (int i = 0; i < _charactersList.Count; i++)
        {
            var portraitData = _charactersList[i];

            _saveLoadServise.Save($"{_saveKey}/{i}", new SaveData.CharactersPortraiteData()
            {
                CharacterType = portraitData.CharacterType,
                Sprite = portraitData.Image.sprite,
                Position = portraitData.Position,
                Name = portraitData.Name,
                PositionOffset = portraitData.PositionOffset,
                ScaleOffset = portraitData.ScalseOffset
            });
        }
    }

    public void Load()
    {
        int characterCount = _saveLoadServise.Load<SaveData.IntData>(_saveKey).Int;

        for (int i = 0; i < characterCount; i++)
        {
            var character = _saveLoadServise.Load<SaveData.CharactersPortraiteData>($"{_saveKey}/{i}");

            CharacterModel characterModel = new CharacterModel()
                {
                    CharacterType = character.CharacterType,
                    Name = character.Name,
                    Sprite = character.Sprite,
                    PositionType = character.Position,
                    PositionOffset = character.PositionOffset,
                    ScaleOffset = character.ScaleOffset
                };

            Show(characterModel);
        }
    }

    private class CharacterModel : ICharacterPortraitModel
    {
        public CharacterType CharacterType { get; set; }
        public string Name { get; set; }
        public Sprite Sprite { get; set; }
        public CharacterPortraitPosition PositionType { get; set; }

        public Vector2 PositionOffset { get; set; }

        public Vector3 ScaleOffset { get; set; }
    }
}
