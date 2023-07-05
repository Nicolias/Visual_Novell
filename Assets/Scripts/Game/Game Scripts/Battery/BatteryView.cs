using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BatteryView : MonoBehaviour
{
    [Inject] private Battery _battery;

    [SerializeField] private Image _batteryImage;
    [SerializeField] private TMP_Text _chargeValueText;

    [SerializeField] private Sprite _lowBetterySprite, _midleBetterySprite, _fullBetterySprite;
    

    private void OnEnable()
    {
        _battery.OnValueChanged += UpdateUI;
        UpdateUI(_battery.ChargeLevel);
    }

    private void OnDisable()
    {
        _battery.OnValueChanged -= UpdateUI;
    }

    private void UpdateUI(int value)
    {
        switch (_battery.ChargeLevel)
        {
            case >= 75:
                _batteryImage.sprite = _fullBetterySprite;
                break;
            case >= 25:
                _batteryImage.sprite = _midleBetterySprite;
                break;
            case >= 0:
                _batteryImage.sprite = _lowBetterySprite;
                break;
        }

        _chargeValueText.text = $"{value}%";
    }
}
