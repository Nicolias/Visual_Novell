using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Factory.Cells
{
    public class CellsFactoryCreater : MonoBehaviour
    {
        [Inject] private DiContainer _di;
        [SerializeField] private CellView _cellViewTemplate;

        public ICellsFactory<T> CreateCellsFactory<T>() where T : IDataForCell
        {
            return new CellsFactory<T>(_di, _cellViewTemplate);
        }

        public ISuppercellsFactory<T> CreateSuppercellsFactory<T>() where T : IDataForCell
        {
            return new CellsFactory<T>(_di, _cellViewTemplate);
        }

        private class CellsFactory<T> : ICellsFactory<T>, ISuppercellsFactory<T> where T : IDataForCell
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
                    CellView newCellView = CreateCellView(cellsContainer, dataForCell);

                    Cell<T> cell = new Cell<T>(dataForCell, newCellView);
                    cells.Add(cell);
                }

                return cells;
            }

            public SupperCell<T> CreateSupperCellView(T dataForCell, Transform cellsContainer)
            {
                CellView newCellView = CreateCellView(cellsContainer, dataForCell);
                SupperCell<T> supperCell = new SupperCell<T>(dataForCell, newCellView);
                newCellView.ChangeSubcellsEnable(false);

                return supperCell;
            }

            private CellView CreateCellView(Transform cellsContainer, T dataForCell)
            {
                CellView newCellView = _di.InstantiatePrefabForComponent<CellView>(_cellViewTemplate, cellsContainer);
                newCellView.Initialize(dataForCell.ToString());
                return newCellView;
            }
        }
    }
}