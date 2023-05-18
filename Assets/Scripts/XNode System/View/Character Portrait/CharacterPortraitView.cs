using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortraitView : MonoBehaviour, ICharacterPortraitView
{
    [SerializeField] private Image _prefab;
    [SerializeField] private Transform[] _position;

    private List<CharacterPortraitData> _charactersList;
    private Color[] _colors;

    private void Start()
    {
        _charactersList = new List<CharacterPortraitData>(_position.Length);
        _colors = new Color[2] { new Color(1, 1, 1, 1), new Color(1, 1, 1, 0) };
    }

    public void Show(string name, Sprite sprite, CharacterPortraitPosition position)
    {
        Image newCharacterImage = Instantiate(_prefab, _position[(int)position]);
        CharacterPortraitData newCharacterPortraitData = new(name, newCharacterImage, position);

        if (Replace(newCharacterPortraitData, out CharacterPortraitData existing) == false)
        {
            newCharacterImage.name = name;
            newCharacterImage.sprite = sprite;
            newCharacterImage.color = _colors[0];

            _charactersList.Add(newCharacterPortraitData);
        }
        else
        {
            if (existing.Position == CharacterPortraitPosition.Delete)
            {
                Destroy(existing.Image.gameObject);
                return;
            }

            existing.Image.sprite = sprite;
            existing.Image.transform.position = _position[(int)existing.Position].position;
        }
    }

    private bool Replace(CharacterPortraitData newCharacterPortraitData, out CharacterPortraitData existingBox)
    {
        existingBox = null;

        for (int i = 0; i < _charactersList.Count; i++)
        {
            if (newCharacterPortraitData.Name == _charactersList[i].Name 
                && newCharacterPortraitData.Position != _charactersList[i].Position)
            {
                _charactersList[i].SetPosition(newCharacterPortraitData.Position);
                existingBox = _charactersList[i];
                return true;
            }
        }

        return false;
    }
}
