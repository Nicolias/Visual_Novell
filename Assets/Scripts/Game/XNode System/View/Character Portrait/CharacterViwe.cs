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

    public void Initialize(ICharacterPortraitModel characterData, Meeting meeting)
    {
        _characterType = characterData.CharacterType;
    }

    public void SetInteractable(bool isInteractable)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        _selfImage = GetComponent<Image>();
    }
}