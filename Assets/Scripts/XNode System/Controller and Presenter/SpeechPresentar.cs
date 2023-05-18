using System;
using System.Collections;
using UnityEngine;

public class SpeechPresentar : IPresentar 
{
    public event Action OnComplete;

    private ISpeechModel _model;
    private SpeechView _view;
    private StaticData _staticData;

    public SpeechPresentar(DialogSpeechModel model, DialogSpeechView view, StaticData staticData)
    {
        _model = model;
        _view = view;
        _staticData = staticData;
    }

    public SpeechPresentar(MonologSpeechModel model, MonologSpeechView view, StaticData staticData)
    {
        _model = model;
        _view = view;
        _staticData = staticData;
    }

    public void Execute()
    {
        _view.OnClick += OnCallBackView;

        if (_view.gameObject.activeInHierarchy)
        {
            _view.ShowSmooth(_model);

            if (_model.IsImmediatelyNextNode)
                OnComplete?.Invoke();
        }
        else
        {
            _staticData.StartCoroutine(WaitUntilAndInvoke(() =>
            {
                _view.ShowSmooth(_model);

                if (_model.IsImmediatelyNextNode)
                    OnComplete?.Invoke();
            }));
        }
    }

    private void OnCallBackView()
    {
        if (_view.ShowStatus == ShowTextStatus.Showing)
        {
            if (_view.gameObject.activeInHierarchy)
            {
                _view.Show(_model);
                return;
            }
            else
            {
                _staticData.StartCoroutine(WaitUntilAndInvoke(() =>
                {
                    _view.Show(_model);
                    return;
                }));
            }
        }

        _view.OnClick -= OnCallBackView;
        OnComplete?.Invoke();
    }


    private IEnumerator WaitUntilAndInvoke(Action action)
    {
        yield return new WaitUntil(() => _view.gameObject.activeInHierarchy == true);
        action.Invoke();
    }
}
