using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New location", menuName = "Location/Location")]
public class LocationSO : ScriptableObject, IDataForCell
{
    [SerializeField] private Dictionary.Dictionary<TimesOfDayType, Vector2> _locationOffsetByDeyTime;
    [SerializeField] private Dictionary.Dictionary<TimesOfDayType, Sprite> _locationSpritesByDeyTime;

    [SerializeField] private List<Vector2> _artifactPositionVariations;
    [SerializeField] private List<ItemForCollection> _itemsOnLocation = new List<ItemForCollection>();

    [Header("Настройки положения персонажа на локации")]
    [SerializeField] private Vector2 _characterPosition;
    [SerializeField] private Vector3 _characterScale;

    [SerializeField] private CharacterPoseType _characterPoseType;
    [Header("")]

    [SerializeField] private List<PastimeOnLocationType> _actionsOnLocation;
    
    private TimesOfDayServise _timesOfDayServise;

    [field: SerializeField] public Sprite DefultSprite { get; private set; }
    [field: SerializeField] public bool IsForArtifacts { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Superlocation Superlocation { get; private set; }

    public bool IsAvailable => _locationSpritesByDeyTime.Contains(_timesOfDayServise.GetCurrentTimesOfDay());

    public IEnumerable<ItemForCollection> ItemsOnLocation => _itemsOnLocation;
    public IEnumerable<PastimeOnLocationType> ActionsList => _actionsOnLocation;


    public void Initialize(TimesOfDayServise timesOfDayServise)
    {
        _timesOfDayServise = timesOfDayServise;
    }

    public CharacterOnLocationData Get(CharacterSO character)
    {
        Vector2 characterOffset = _characterPosition;

        if (_locationOffsetByDeyTime.TryGet(_timesOfDayServise.GetCurrentTimesOfDay(), out Vector2 characterOffsetByDayTime))
            characterOffset += characterOffsetByDayTime;

        if (character.Images.TryGet(_characterPoseType, out Sprite characterSprite))
        {
            return new CharacterOnLocationData(character.Type, character.Name, characterSprite, this,
                    _characterPoseType == CharacterPoseType.Staying ? CharacterPortraitPosition.Right : CharacterPortraitPosition.FreePosition,
                    characterOffset, _characterScale);
        }
        else
        {
            throw new InvalidOperationException("Эта локация не предназначена для приглашения.");
        }
    }

    public bool TryGetByCurrentTime(out Sprite sprite)
    {
        return _locationSpritesByDeyTime.TryGet(_timesOfDayServise.GetCurrentTimesOfDay(), out sprite);
    }

    public Vector2 GetRandomArtifactPosition()
    {
        return _artifactPositionVariations[UnityEngine.Random.Range(0, _artifactPositionVariations.Count)];
    }

    public void Destory(IEnumerable<ItemForCollectionView> itemsView)
    {
        foreach (var itemView in itemsView)
            if(itemView.Data is Artifact)
                Destroy(itemView.gameObject);
    }
}
