using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CharacterPortraitView : MonoBehaviour, ICharacterPortraitView, ISaveLoadObject
{
    public event Action OnComplite;

    [Inject] private SaveLoadServise _saveLoadServise;

    [Inject] private BackgroundView _backgroundView;

    [SerializeField] private Image _prefab;
    [SerializeField] private Transform[] _positions;

    private List<CharacterPortraitData> _charactersList;
    private Color[] _colors;

    private const string _saveKey = "CharacterPortrait";

    private void Awake()
    {
        _colors = new Color[2] { new Color(1, 1, 1, 1), new Color(1, 1, 1, 0) };
    }

    private void OnEnable()
    {
        _charactersList = new();

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        Save();
    }

    public void Show(ICharacterPortraitModel character)
    {
        if (CheckNeededCreateNewCharacter(character.CharacterType, out CharacterPortraitData exist))
        {
            Image newCharacterImage = InstantiateCharacter(character.Name, character.Sprite, character.Position);

            CharacterPortraitData newCharacterPortraitData = new(character, newCharacterImage);

            DOTween.Sequence()
                .Append(newCharacterImage.DOColor(new(1, 1, 1, 0), 0))
                .Append(newCharacterImage.DOColor(new(1, 1, 1, 1), 0.5f))
                .AppendCallback(() => OnComplite?.Invoke())
                .Play();

            _charactersList.Add(newCharacterPortraitData);
        }
        else
        {
            if (character.Position == CharacterPortraitPosition.Delete)
            {
                DOTween.Sequence()
                .Append(exist.Image.DOColor(new(1, 1, 1, 0), 0.5f))
                .AppendCallback(() => OnComplite?.Invoke())
                .Play();

                _charactersList.Remove(exist);
                Destroy(exist.Image.gameObject);
                return;
            }

            DOTween.Sequence()
                .Append(exist.Image.DOColor(new(1, 1, 1, 0), 0.2f))
                .AppendCallback(() =>
                {
                    exist.Image.sprite = character.Sprite;
                    exist.SetPosition(character.Position);
                    exist.Image.transform.SetParent(_positions[(int)exist.Position]);
                })
                .Append(exist.Image.DOColor(new(1, 1, 1, 1), 0.2f))
                .AppendCallback(() => OnComplite?.Invoke())
                .Play();
        }
    }

    private bool CheckNeededCreateNewCharacter(CharacterType characterType, out CharacterPortraitData exist)
    {
        exist = null;

        for (int i = 0; i < _charactersList.Count; i++)
        {
            if (_charactersList[i].CharacterType == characterType)
            {
                exist = _charactersList[i];
                return false;
            }
        }

        return true;
    }

    private Image InstantiateCharacter(string characterName, Sprite characterPortraitSprite, 
        CharacterPortraitPosition characterPortraitPosition)
    {
        Image newCharacterImage = Instantiate(_prefab, _positions[(int)characterPortraitPosition]);

        newCharacterImage.name = characterName;
        newCharacterImage.sprite = characterPortraitSprite;
        newCharacterImage.color = _colors[0];

        return newCharacterImage;
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
            });
        }
    }

    public void Load()
    {
        int characterCount = _saveLoadServise.Load<SaveData.IntData>(_saveKey).Int;

        for (int i = 0; i < characterCount; i++)
        {
            var character = _saveLoadServise.Load<SaveData.CharactersPortraiteData>($"{_saveKey}/{i}");
            Show(new CharacterModel(character.CharacterType, 
                character.Name, character.Sprite, character.Position));
        }
    }

    private class CharacterModel : ICharacterPortraitModel
    {
        public CharacterType CharacterType { get; private set; }
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }
        public CharacterPortraitPosition Position { get; private set; }

        public CharacterModel(CharacterType characterType, string name, 
            Sprite sprite, CharacterPortraitPosition position)
        {
            CharacterType = characterType;
            Name = name;
            Sprite = sprite;
            Position = position;
        }
    }
}
