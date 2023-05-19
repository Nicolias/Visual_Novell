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

    public void Show(string name, Sprite sprite, CharacterPortraitPosition position)
    {
        if (CheckNeededCreateNewCharacter(name, out CharacterPortraitData exist))
        {
            Image newCharacterImage = Instantiate(_prefab, _positions[(int)position]);

            newCharacterImage.name = name;
            newCharacterImage.sprite = sprite;
            newCharacterImage.color = _colors[0];

            CharacterPortraitData newCharacterPortraitData = new(name, newCharacterImage, position);

            _charactersList.Add(newCharacterPortraitData);
        }
        else
        {
            if (exist.Position == CharacterPortraitPosition.Delete)
            {
                Destroy(exist.Image.gameObject);
                return;
            }

            exist.Image.sprite = sprite;
            exist.SetPosition(position);
            exist.Image.transform.SetParent(_positions[(int)exist.Position]);
        }
    }

    private bool CheckNeededCreateNewCharacter(string name, out CharacterPortraitData exist)
    {
        exist = null;

        for (int i = 0; i < _charactersList.Count; i++)
        {
            if (_charactersList[i].Name == name)
            {
                exist = _charactersList[i];
                return false;
            }
        }

        return true;
    }
}
