using System;

public class SupperCell<T> : AbstractCell<T>
{
    private CellView _cellView;

    public SupperCell(T data, CellView cellView) : base(data, cellView)
    {
        _cellView = cellView;
    }

    public CellView View => _cellView;

    public event Action<CellView> Clicked;

    protected override void OnCellClicked()
    {
        Clicked?.Invoke(_cellView);
    }
}