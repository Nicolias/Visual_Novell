using DG.Tweening;
using Zenject;

public class DUX : WindowInSmartphone
{
    [Inject] private DUXWindow _duxWindow;

    private Sequence _sequence;

    public void OpenAccesToDUX()
    {
        SetOpenButtonEnabled(true);

        _sequence = DOTween.Sequence();

        _sequence
            .Append(transform.DOScale(1.2f, 1.5f))
            .Append(transform.DOScale(1, 1.5f))
            .SetLoops(-1)
            .Play();
    }

    protected override void OnOpenButtonClicked()
    {
        _duxWindow.Open();

        if (_sequence != null)
            _sequence.Pause();
    }
}