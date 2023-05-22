using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DUX : MonoBehaviour
{
    [SerializeField] private DUXWindow _duxWindow;
    [SerializeField] private Button _openDUXButton;

    private Sequence _sequence;

    private void OnEnable()
    {
        _openDUXButton.onClick.AddListener(() =>
        {
            _duxWindow.Open();

            if(_sequence != null)
                _sequence.Pause();
        });
    }

    private void OnDisable()
    {
        _openDUXButton.onClick.RemoveAllListeners();
    }

    public void OpenAccesToDUX()
    {
        _openDUXButton.enabled = true;

        _sequence = DOTween.Sequence();

        _sequence
            .Append(transform.DOScale(1.2f, 1.5f))
            .Append(transform.DOScale(1, 1.5f))
            .SetLoops(-1)
            .Play();
    }
}
