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
        private ICellsFactory<Location> _locationCellsFactory;

        [SetUp]
        public void SetUp()
        {
            _locationCellsFactory = Substitute.For<ICellsFactory<Location>>();
            CellsFactoryCreater cellsFactoryCreater = Substitute.For<CellsFactoryCreater>();
            cellsFactoryCreater.CreateCellsFactory<Location>().Returns(_locationCellsFactory);

            _map = new GameObject().AddComponent<Map>();
            _map.Construct(null, cellsFactoryCreater, null, Substitute.For<IChoicePanelFactory>(), null);
        }

        [TestCase(1), TestCase(2)]
        public void WhenLocationAdded_AndLocationInMapEmpty_ThenLocationCellsInMapShoulBe(int locationCount)
        {
            // Arrange.

            // Act.
            for (int i = 0; i < locationCount; i++)
            {
                List<Location> locations = new List<Location>();
                locations.Add(ScriptableObject.CreateInstance<Location>());

                StubCells(1, locations);

                _map.Add(locations);
            }

            // Assert.
            _map.LocationCellsCount.Should().Be(locationCount);
        }


        [Test]
        public void WhenLocationRemoved_AndWasOneLocation_ThenLocationCountShouldEmpty()
        {
            // Arrange.
            List<Location> locations = new List<Location>();
            locations.Add(ScriptableObject.CreateInstance<Location>());

            StubCells(1, locations);
            
            _map.Add(locations);

            // Act.
            _map.Remove(locations);

            // Assert.
            _map.LocationCellsCount.Should().Be(0);
            _map.LocationsCount.Should().Be(0);
        }

        public void StubCells(int cellsCount, List<Location> locations)
        {
            List<Cell<Location>> locationCells = new List<Cell<Location>>();

            for (int i = 0; i < cellsCount; i++)
            {
                Cell<Location> cell = new Cell<Location>(locations[i], new GameObject().AddComponent<CellView>());
                locationCells.Add(cell);
                _locationCellsFactory.CreateCellsView(locations, null).Returns(locationCells);
            }
        }
    }
}
