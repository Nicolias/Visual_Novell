using System;

public abstract class AbstractCell<T> : IDisposable
{
    private readonly T _selfData;
    private readonly CellView _view;

    public AbstractCell(T data, CellView cellView)
    {
        _selfData = data;
        _view = cellView;

        cellView.Clicked += OnCellClicked;
        cellView.Initialize(data.ToString());
    }

    public T Data => _selfData;

    public virtual void Dispose()
    {
        _view.Clicked -= OnCellClicked;
        _view.Destory();
    }

    public void SetInteractable(bool isInteractable)
    {
        _view.SetInteractable(isInteractable);
    }

    protected abstract void OnCellClicked();
}
