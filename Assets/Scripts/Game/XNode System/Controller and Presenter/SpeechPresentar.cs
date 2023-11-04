using System;
using System.Collections;
using UnityEngine;

public abstract class SpeechPresentar : IPresentar 
{
    private StaticData _staticData;

    private ISpeechModel _model;
    private SpeechView _view;

    public SpeechPresentar(ISpeechModel model, SpeechView view, StaticData staticData)
    {
        _model = model;
        _view = view;
        _staticData = staticData;
    }    

    public event Action Complete;

    public void Execute()
    {
        if (_staticData.IsSkipDialog)
        {
            ShowSpeech();
            Complete?.Invoke();
            return;
        }

        _view.Clicked += OnViewClick;

        if (_view.gameObject.activeInHierarchy)
        {
            ShowSpeechSmooth();

            if (_model.IsImmediatelyNextNode)
                Complete?.Invoke();
        }
        else
        {
            _staticData.StartCoroutine(WaitUntilAndInvoke(() =>
            {
                ShowSpeechSmooth();

                if (_model.IsImmediatelyNextNode)
                    Complete?.Invoke();
            }));
        }
    }

    private void OnViewClick()
    {
        if (_view.ShowStatus == ShowTextStatus.Showing)
        {
            if (_view.gameObject.activeInHierarchy)
            {
                ShowSpeech();
                return;
            }
            else
            {
                _staticData.StartCoroutine(WaitUntilAndInvoke(() =>
                {
                    ShowSpeech();
                    return;
                }));
            }
        }

        _view.Clicked -= OnViewClick;
        Complete?.Invoke();
    }

    protected abstract void ShowSpeechSmooth();
    protected abstract void ShowSpeech();

    private IEnumerator WaitUntilAndInvoke(Action action)
    {
        yield return new WaitUntil(() => _view.gameObject.activeInHierarchy == true);
        action.Invoke();
    }
}