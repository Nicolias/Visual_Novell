using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public abstract class SpeechView : MonoBehaviour, ISpeechView
{
    public event Action OnClick;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private ClickerZone _clickerZone;

    [SerializeField] private float _timeBetweenShowNewCharInDialog;
    [SerializeField] private TMP_Text _speechText;

    private ShowTextStatus _showStatus;
    private WaitForSeconds _waitForSeconds;
    private StringBuilder _stringBuilder;

    public ShowTextStatus ShowStatus => _showStatus;

    private void OnEnable()
    {
        _clickerZone.OnClick += OnCallBack;
    }

    private void OnDisable()
    {
        _clickerZone.OnClick -= OnCallBack;
    }

    private void Awake()
    {
        _waitForSeconds = new(_timeBetweenShowNewCharInDialog);
        _stringBuilder = new();
    }

    public virtual void Show(ISpeechModel model)
    {
        _selfCanvas.gameObject.SetActive(true);

        _stringBuilder.Clear();
        _showStatus = ShowTextStatus.Complete;

        StopAllCoroutines();

        _speechText.text = model.Text;
    }

    public virtual void ShowSmooth(ISpeechModel model)
    {
        _selfCanvas.gameObject.SetActive(true);

        StartCoroutine(ShowingSpeechSmooth(model.Text));
    }

    public void Hide()
    {
        _selfCanvas.gameObject.SetActive(false);
    }

    private void OnCallBack()
    {
        OnClick?.Invoke();
    }

    protected IEnumerator ShowingSpeechSmooth(string speechText)
    {
        _showStatus = ShowTextStatus.Showing;

        foreach (char charInDialog in speechText)
        {
            yield return _waitForSeconds;
            _stringBuilder.Append(charInDialog);

            _speechText.text = _stringBuilder.ToString();
        }

        _showStatus = ShowTextStatus.Complete;
        _stringBuilder.Clear();
    }
}