using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CharacterView : MonoBehaviour
{
    private Image _selfImage;


    private void Awake()
    {
        _selfImage = GetComponent<Image>();

    }
}