using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Factory.Cells
{
    public class CellsFactoryCreater : MonoBehaviour
    {
        [Inject] private DiContainer _di;
        [SerializeField] private CellView _cellViewTemplate;

        public virtual ICellsFactory<T> CreateCellsFactory<T>() where T : IDataForCell
        {
            return new CellsFactory<T>(_di, _cellViewTemplate);
        }

        private class CellsFactory<T> : ICellsFactory<T> where T : IDataForCell
        {
            private DiContainer _di;
            private CellView _cellViewTemplate;

            public CellsFactory(DiContainer di, CellView cellViewTemplate)
            {
                _di = di;
                _cellViewTemplate = cellViewTemplate;
            }

            public List<Cell<T>> CreateCellsView(IEnumerable<T> dataForCells, Transform cellsContainer)
            {
                List<Cell<T>> cells = new List<Cell<T>>();

                foreach (var dataForCell in dataForCells)
                {
                    CellView newCellView = _di.InstantiatePrefabForComponent<CellView>(_cellViewTemplate, cellsContainer);
                    newCellView.Initialize(dataForCell.Name);

                    Cell<T> cell = new Cell<T>(dataForCell, newCellView);
                    cells.Add(cell);
                }

                return cells;
            }
        }
    }
}