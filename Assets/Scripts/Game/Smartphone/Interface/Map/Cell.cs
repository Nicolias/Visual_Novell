using System;

public class Cell<T> : AbstractCell<T>
{
    public Cell(T data, CellView cellView) : base(data, cellView){}

    public event Action<T> Clicked;

    protected override void OnCellClicked()
    {
        Clicked?.Invoke(Data);
    }
}
