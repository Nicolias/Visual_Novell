using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortraitView : MonoBehaviour, ICharacterPortraitView
{
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
        }
        else
        {
            if (characterPortrait.Position == CharacterPortraitPosition.Delete)
            {
                _charactersList.Remove(exist);
                Destroy(exist.Image.gameObject);
                return;
            }

            exist.Image.sprite = characterPortrait.Sprite;
            exist.SetPosition(characterPortrait.Position);
            exist.Image.transform.SetParent(_positions[(int)exist.Position]);
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
