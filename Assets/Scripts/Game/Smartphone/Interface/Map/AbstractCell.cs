using System;

public abstract class AbstractCell<T> : IDisposable
{
    private readonly T _selfData;
    private readonly CellView CellView;

    public AbstractCell(T data, CellView cellView)
    {
        _selfData = data;
        CellView = cellView;
        cellView.Clicked += OnCellClicked;
    }

    public T Data => _selfData;

    public void Dispose()
    {
        CellView.Clicked -= OnCellClicked;
        CellView.Destory();
    }

    protected abstract void OnCellClicked();
}
