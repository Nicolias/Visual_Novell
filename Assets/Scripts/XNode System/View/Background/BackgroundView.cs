using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void Replace(Sprite sprite)
    {
        _image.sprite = sprite;

        if (_image.sprite == null)
            _image.color = new(1, 1, 1, 0);
        else
            _image.color = new(1, 1, 1, 1);
    }
}
