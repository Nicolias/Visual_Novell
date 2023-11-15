using System;

public abstract class AbstractCell<T> : IDisposable
{
    private readonly T _selfData;
    private readonly CellView View;

    public AbstractCell(T data, CellView cellView)
    {
        _selfData = data;
        View = cellView;
        cellView.Clicked += OnCellClicked;
    }

    public T Data => _selfData;

    public void Dispose()
    {
        View.Clicked -= OnCellClicked;
        View.Destory();
    }

    public void SetInteractable(bool isInteractable)
    {
        View.SetInteractable(isInteractable);
    }

    protected abstract void OnCellClicked();
}
