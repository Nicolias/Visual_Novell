using System;

public class Cell<T> : IDisposable
{
    private readonly T _selfData;
    private readonly CellView _cellView;

    public Cell(T data, CellView cellView)
    {
        _selfData = data;
        _cellView = cellView;
        cellView.Clicked += OnCellClicked;
    }

    public T Data => _selfData;

    public event Action<T> Clicked;

    public void Dispose()
    {
        _cellView.Clicked -= OnCellClicked;
        _cellView.Destory();
    }

    private void OnCellClicked()
    {
        Clicked?.Invoke(_selfData);
    }
}