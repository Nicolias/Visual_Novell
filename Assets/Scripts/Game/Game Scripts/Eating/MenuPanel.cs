using Factory.Cells;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace EatingSystem
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        [SerializeField] private List<EatingProduct> _products = new List<EatingProduct>();
        [SerializeField] private Transform _productsCellsContainer;

        private ICellsFactory<EatingProduct> _productCellsFactory;
        private List<Cell<EatingProduct>> _productCells;

        public event Action<EatingProduct> ProductSelected;
        public event Action Closed;

        [Inject]
        public void Construct(CellsFactoryCreater cellsFactory)
        {
            _productCellsFactory = cellsFactory.CreateCellsFactory<EatingProduct>();
        }

        public void Open()
        {
            if (_productCells == null)
                _productCells = _productCellsFactory.CreateCellsView(_products, _productsCellsContainer);

            foreach (var productCell in _productCells)
                productCell.Clicked += OnProductCellClicked;

            _closeButton.onClick.AddListener(Close);
        }

        private void Close()
        {
            foreach (var productCell in _productCells)
                productCell.Clicked -= OnProductCellClicked;

            _closeButton.onClick.RemoveListener(Close);

            Closed?.Invoke();
        }

        private void OnProductCellClicked(EatingProduct product)
        {
            ProductSelected?.Invoke(product);
        }
    }
}