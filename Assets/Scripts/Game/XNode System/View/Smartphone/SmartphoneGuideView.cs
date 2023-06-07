using System;
using UnityEngine;
using UnityEngine.UI;

public class SmartphoneGuideView : MonoBehaviour
{
    public event Action OnComplete;

    [SerializeField] private Button _closebutton, _smartphoneOpenButton;

    private void OnEnable()
    {
        _closebutton.onClick.AddListener(() =>
        {
            OnComplete?.Invoke();
            gameObject.SetActive(false);
        });

        _smartphoneOpenButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _closebutton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
