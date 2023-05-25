using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortraitView : MonoBehaviour, ICharacterPortraitView
{
    public event Action OnComplite;

    [SerializeField] private Image _prefab;
    [SerializeField] private Transform[] _positions;

    private List<CharacterPortraitData> _charactersList;
    private Color[] _colors;

    private void Start()
    {
        _charactersList = new();
        _colors = new Color[2] { new Color(1, 1, 1, 1), new Color(1, 1, 1, 0) };
    }

    public void Show(ICharacterPortraitModel characterPortrait)
    {
        if (CheckNeededCreateNewCharacter(characterPortrait.CharacterType, out CharacterPortraitData exist))
        {
            Image newCharacterImage = Instantiate(_prefab, _positions[(int)characterPortrait.Position]);

            newCharacterImage.name = name;
            newCharacterImage.sprite = characterPortrait.Sprite;
            newCharacterImage.color = _colors[0];

            CharacterPortraitData newCharacterPortraitData = new(characterPortrait, newCharacterImage);

            _charactersList.Add(newCharacterPortraitData);

            DOTween.Sequence()
                .Append(newCharacterImage.DOColor(new(1, 1, 1, 0), 0))
                .Append(newCharacterImage.DOColor(new(1, 1, 1, 1), 0.5f))
                .AppendCallback(() => OnComplite?.Invoke())
                .Play();
        }
        else
        {
            if (characterPortrait.Position == CharacterPortraitPosition.Delete)
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
                    exist.Image.sprite = characterPortrait.Sprite;
                    exist.SetPosition(characterPortrait.Position);
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
}
