using System;
using UnityEngine;

public class GetterItemController : IController
{
    public event Action OnComplete;

    private AccureItemPanel _view;

    public GetterItemController(AccureItemPanel view)
    {
        _view = view;
    }

    public void Execute()
    {
        _view.gameObject.SetActive(true);
        _view.OnClosed += OnPanelClose;
    }

    private void OnPanelClose()
    {
        _view.OnClosed -= OnPanelClose;

        OnComplete?.Invoke();
    }
}