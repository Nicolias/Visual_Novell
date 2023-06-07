using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuessNumberPanel : MonoBehaviour
{
    public event Action<int> OnValueSelected;

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _acepetButton;

    private void OnEnable()
    {
        _acepetButton.onClick.AddListener(() =>
        {
            OnValueSelected?.Invoke((int)_slider.value);
            ClosePanel();
        });

        _slider.onValueChanged.AddListener((value) => _text.text = ((int)value).ToString());
    }

    private void OnDisable()
    {
        _acepetButton.onClick.RemoveAllListeners();

        _slider.onValueChanged.RemoveAllListeners();
    }

    public void OpenPanel((int, int) bounds, string text)
    {
        gameObject.SetActive(true);

        _slider.minValue = bounds.Item1;
        _slider.value = bounds.Item1;
        _slider.maxValue = bounds.Item2;

        _text.text = text;
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}