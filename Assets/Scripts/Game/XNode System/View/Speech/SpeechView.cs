using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public abstract class SpeechView : MonoBehaviour, ISpeechView
{
    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private ClickerZone _clickerZone;

    [SerializeField] private float _skipShowAnimationCooldown;
    [SerializeField] private float _timeBetweenShowNewCharInDialog;
    [SerializeField] private TMP_Text _speechText;

    private ShowTextStatus _showStatus;
    private WaitForSeconds _waitForSeconds;
    private StringBuilder _stringBuilder;
    private float _nextSkipShowAnimationTime;

    public ShowTextStatus ShowStatus => _showStatus;

    public event Action Clicked;

    private void Awake()
    {
        _waitForSeconds = new(_timeBetweenShowNewCharInDialog);
        _stringBuilder = new();
    }

    private void OnEnable()
    {
        _clickerZone.Clicked += OnClick;
    }

    private void OnDisable()
    {
        _clickerZone.Clicked -= OnClick;
    }

    public void ShowSmooth(ISpeechModel model)
    {
        _nextSkipShowAnimationTime = Time.time + _skipShowAnimationCooldown;

        _selfCanvas.enabled = true;

        StartCoroutine(ShowingSpeechSmooth(model.Text));
    }

    public void Show(ISpeechModel model)
    {
        if (Time.time < _nextSkipShowAnimationTime)
            return;

        _selfCanvas.enabled = true;

        _stringBuilder.Clear();
        _showStatus = ShowTextStatus.Complete;

        StopAllCoroutines();

        _speechText.text = model.Text;
    }

    public void Hide()
    {
        _selfCanvas.enabled = false;
    }

    private void OnClick()
    {
        if (ShowStatus == ShowTextStatus.Complete)
            Hide();

        Clicked?.Invoke();
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