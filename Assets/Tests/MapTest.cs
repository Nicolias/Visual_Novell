using System.Collections.Generic;
using Factory.Cells;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class MapTest
    {
        private Map _map;
        private ICellsFactory<LocationSO> _locationCellsFactory;

        [SetUp]
        public void SetUp()
        {
            _locationCellsFactory = Substitute.For<ICellsFactory<LocationSO>>();
            CellsFactoryCreater cellsFactoryCreater = Substitute.For<CellsFactoryCreater>();
            cellsFactoryCreater.CreateCellsFactory<LocationSO>().Returns(_locationCellsFactory);

            _map = new GameObject().AddComponent<Map>();
            //_map.Construct(null, cellsFactoryCreater, null, Substitute.For<IChoicePanelFactory>());
        }

        [TestCase(1), TestCase(2)]
        public void WhenLocationAdded_AndLocationInMapEmpty_ThenLocationCellsInMapShoulBe(int locationCount)
        {
            // Arrange.

            // Act.
            for (int i = 0; i < locationCount; i++)
            {
                List<LocationSO> locations = new List<LocationSO>();
                locations.Add(ScriptableObject.CreateInstance<LocationSO>());

                StubCells(1, locations);

                //_map.Add(locations);
            }

            // Assert.
            _map.LocationCellsCount.Should().Be(locationCount);
        }


        [Test]
        public void WhenLocationRemoved_AndWasOneLocation_ThenLocationCountShouldEmpty()
        {
            // Arrange.
            List<LocationSO> locations = new List<LocationSO>();
            locations.Add(ScriptableObject.CreateInstance<LocationSO>());

            StubCells(1, locations);
            
            //_map.Add(locations);

            // Act.
            //_map.Remove(locations);

            // Assert.
            _map.LocationCellsCount.Should().Be(0);
            _map.LocationsCount.Should().Be(0);
        }

        public void StubCells(int cellsCount, List<LocationSO> locations)
        {
            List<Cell<LocationSO>> locationCells = new List<Cell<LocationSO>>();

            for (int i = 0; i < cellsCount; i++)
            {
                Cell<LocationSO> cell = new Cell<LocationSO>(locations[i], new GameObject().AddComponent<CellView>());
                locationCells.Add(cell);
                _locationCellsFactory.CreateCellsView(locations, null).Returns(locationCells);
            }
        }
    }
}
