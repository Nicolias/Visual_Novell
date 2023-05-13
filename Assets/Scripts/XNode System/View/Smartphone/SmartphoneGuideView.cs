using System;
using UnityEngine;
using UnityEngine.UI;

public class SmartphoneGuideView : MonoBehaviour
{
    public event Action OnComplete;

    [SerializeField] private Button _closebutton;

    private void OnEnable()
    {
        _closebutton.onClick.AddListener(() =>
        {
            OnComplete?.Invoke();
            gameObject.SetActive(false);
        });
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
