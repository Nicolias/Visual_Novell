using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuTimeDependent: MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Sprite _morningSprite;
    [SerializeField] private Sprite _daySprite;
    [SerializeField] private Sprite _eveningSprite;
    [SerializeField] private Sprite _nightSprite;

    [SerializeField] private TMP_Text _timeText;

    private TimesOfDayServise _timesOfDayServise;

    [Inject]
    public void Construct(TimesOfDayServise timesOfDayServise)
    {
        _timesOfDayServise = timesOfDayServise;
    }

    private void Update()
    {
        ChangeBackground(_timesOfDayServise.GetCurrentTimesOfDay());
        _timeText.text = $"{_timesOfDayServise.CurrentTime:HH}:{_timesOfDayServise.CurrentTime:mm}";
    }

    private void ChangeBackground(TimesOfDayType timesOfDayType)
    {
        switch (timesOfDayType)
        {
            case TimesOfDayType.Morning:
                _background.sprite = _morningSprite;
                break;
            case TimesOfDayType.Day:
                _background.sprite = _daySprite;
                break;
            case TimesOfDayType.Evning:
                _background.sprite = _eveningSprite;
                break;
            case TimesOfDayType.Night:
                _background.sprite = _nightSprite;
                break;
            default:
                break;
        }
    }
}