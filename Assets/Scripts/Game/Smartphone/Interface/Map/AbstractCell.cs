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

public class Cell<T> : AbstractCell<T>
{
    public Cell(T data, CellView cellView) : base(data, cellView){}

    public event Action<T> Clicked;

    protected override void OnCellClicked()
    {
        Clicked?.Invoke(Data);
    }
}

public class SupperCell<T> : AbstractCell<T>
{
    private CellView _cellView;

    public SupperCell(T data, CellView cellView) : base(data, cellView)
    {
        _cellView = cellView;
    }

    public CellView Veiw => _cellView;

    protected override void OnCellClicked()
    {
        _cellView.SwitchSubcellsEnable();
    }
}