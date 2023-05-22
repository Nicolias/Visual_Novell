using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccureMoneyView : MonoBehaviour
{
    public event Action OnComplete;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Wallet _wallet;

    [SerializeField] private TMP_Text _log;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(() =>
        {
            OnComplete?.Invoke();
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void AccureMoney(int money)
    {
        _wallet.AccureMoney(money);
        _log.text = $"Было получено {money} Форы";
    }
}