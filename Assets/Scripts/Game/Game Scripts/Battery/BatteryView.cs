using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Dictionary;
using System;

public class BatteryView : MonoBehaviour
{
    [Inject] private Battery _battery;

    [SerializeField] private Image _batteryImage;
    [SerializeField] private TMP_Text _chargeValueText;

    [SerializeField] private Sprite _lowBetterySprite, _midleBetterySprite, _fullBetterySprite;

    [SerializeField] private Dictionary<Vector2, Sprite> _batterySprites;

    private void OnEnable()
    {
        _battery.ValueChanged += UpdateUI;
        UpdateUI(_battery.CurrentValue);
    }

    private void OnDisable()
    {
        _battery.ValueChanged -= UpdateUI;
    }

    private void UpdateUI(int chargyValue)
    {
        for (int i = 0; i < _batterySprites.Count; i++)
        {
            Vector2 chargeRange = _batterySprites.GetKey(i);

            if (chargeRange.x > chargeRange.y)
                throw new InvalidProgramException("В диапазоне зарядов минмальный больше максимального");

            if (GetProcent(_battery.CurrentValue, _battery.MaxValue) >= chargeRange.x)
                if(GetProcent(_battery.CurrentValue, _battery.MaxValue) <= chargeRange.y)
                    _batteryImage.sprite = _batterySprites.GetValue(i);
        }

        _chargeValueText.text = $"{chargyValue}%";
    }

    private int GetProcent(float currentCharge, float maxCharge)
    {
        return (int) (currentCharge / maxCharge * 100f);
    }
}
