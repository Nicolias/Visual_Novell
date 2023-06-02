using UnityEngine;

[System.Serializable]
public class Location
{
    [SerializeField] private BackgroundView _background;
    [SerializeField] private Sprite _backgroundSprite;
    [field: SerializeField] public LocationType LocationType { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    public void Show()
    {
        _background.Replace(_backgroundSprite);
    }
}
