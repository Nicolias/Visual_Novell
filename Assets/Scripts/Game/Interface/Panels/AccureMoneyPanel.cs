using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccureMoneyPanel : MonoBehaviour
{
    public event Action OnClosed;

    [SerializeField] private Button _closeButton;

    [SerializeField] private TMP_Text _log;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(() =>
        {
            OnClosed?.Invoke();
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
    }

    public void PrintAccureMoney(int money)
    {
        gameObject.SetActive(true);
        _log.text = $"Было получено {money} Форы";
    }
}
