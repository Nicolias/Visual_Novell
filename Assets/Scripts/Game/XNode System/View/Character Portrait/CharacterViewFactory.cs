using SaveData;
using UnityEngine;
using UnityEngine.UI;

public class CharacterViewFactory : MonoBehaviour
{
    [SerializeField] private Transform _containerForFreePosition;
    [SerializeField] private Dictionary.Dictionary<CharacterPortraitPosition, Transform> _positions;

    [SerializeField] private CharacterViewForMeeting _characterViewForMeetingPrefab;
    [SerializeField] private CharacterViwe _characterViewPrefab;

    [SerializeField] private Meeting _meeting;

    private Color[] _colors = new Color[2] { new Color(1, 1, 1, 1), new Color(1, 1, 1, 0) };

    public Dictionary.Dictionary<CharacterPortraitPosition, Transform> Positions => _positions.GetCopyOfDictionary();

    public CharacterPortraitData Create(ICharacterPortraitModel character)
    {
        ICharacterView newCharacterView = Instantiate(character, _characterViewForMeetingPrefab);

        Image newCharacterImage = newCharacterView.Image;

        newCharacterImage.name = character.Name;
        newCharacterImage.sprite = character.Sprite;
        newCharacterImage.color = _colors[0];

        CharacterPortraitData newCharacterPortraitData = ContstructCharacterData(character, newCharacterImage);

        return newCharacterPortraitData;
    }

    private ICharacterView Instantiate(ICharacterPortraitModel character, ICharacterView characterPrefab)
    {
        ICharacterView newCharacterView = null;

        if (character.PositionType == CharacterPortraitPosition.FreePosition)
        {
            newCharacterView = Instantiate(characterPrefab.GameObject, _containerForFreePosition).GetComponent<ICharacterView>();

            RectTransform newCharacterTransform = newCharacterView.Image.rectTransform;
            newCharacterTransform.localPosition = character.PositionOffset;
            newCharacterTransform.localScale = character.ScaleOffset;
            newCharacterTransform.sizeDelta = new Vector2(character.Sprite.rect.width, character.Sprite.rect.height);
        }
        else
        {
            if (_positions.TryGet(character.PositionType, out Transform transformForSpawn))
                newCharacterView = Instantiate(characterPrefab.GameObject, transformForSpawn).GetComponent<ICharacterView>();
        }

        newCharacterView.Initialize(character, _meeting);

        return newCharacterView;
    }

    private CharacterPortraitData ContstructCharacterData(ICharacterPortraitModel characterModel, Image characterImage)
    {
        CharacterPortraitData characterPortraitData = new CharacterPortraitData();

        characterPortraitData.CharacterType = characterModel.CharacterType;
        characterPortraitData.Name = characterModel.Name;
        characterPortraitData.Sprite = characterModel.Sprite;
        characterPortraitData.Image = characterImage;
        characterPortraitData.Position = characterModel.PositionType;
        characterPortraitData.PositionOffset = characterModel.PositionOffset;
        characterPortraitData.ScaleOffset = characterModel.ScaleOffset;
        characterPortraitData.Location = characterModel.Location;

        return characterPortraitData;
    }
}