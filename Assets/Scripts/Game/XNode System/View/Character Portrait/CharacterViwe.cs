using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CharacterViwe : MonoBehaviour, ICharacterView
{
    private Image _selfImage;
    private CharacterType _characterType;

    public Image Image => _selfImage;
    public CharacterType Type => _characterType;

    public GameObject GameObject => gameObject;

    public void Initialize(CharacterType character, Meeting meeting, Location location)
    {
        _characterType = character;
    }

    private void Awake()
    {
        _selfImage = GetComponent<Image>();
    }
}