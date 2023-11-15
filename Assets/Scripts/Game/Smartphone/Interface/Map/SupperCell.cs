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