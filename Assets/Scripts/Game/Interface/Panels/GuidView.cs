using System;
using UnityEngine;
using UnityEngine.UI;

public class GuidView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _closeButtone;

    public event Action Closed;

    private void OnEnable()
    {
        _closeButtone.onClick.AddListener(OnClose);
    }

    private void OnDisable()
    {
        _closeButtone.onClick.RemoveListener(OnClose);
    }

    public void Show(Sprite guidSprite)
    {
        _image.sprite = guidSprite;
        gameObject.SetActive(true);
    }

    private void OnClose()
    {
        gameObject.SetActive(false);
        Closed?.Invoke();
    }
}