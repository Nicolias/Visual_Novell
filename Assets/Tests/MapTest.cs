using System.Collections.Generic;
using Factory.CellLocation;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class MapTest
    {
        private Map _map;
        private ILocationCellsFactory _locationCellFactory;

        [SetUp]
        public void SetUp()
        {
            _map = new GameObject().AddComponent<Map>();
            _locationCellFactory = Substitute.For<ILocationCellsFactory>();
            _map.Construct(null, _locationCellFactory, null, null, null);
        }

        [TestCase(1), TestCase(2)]
        public void WhenLocationAdded_AndLocationInMapEmpty_ThenLocationCellsInMapShoulBe(int locationCount)
        {
            // Arrange.

            // Act.
            for (int i = 0; i < locationCount; i++)
            {
                IEnumerable<Location> locations = new List<Location>();
                List<LocationCell> locationCells = new List<LocationCell>();

                locationCells.Add(new GameObject().AddComponent<LocationCell>());
                _locationCellFactory.CreateNewLocationCell(locations, null).Returns(locationCells);
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

            List<LocationCell> locationCells = new List<LocationCell>();
            locationCells.Add(new GameObject().AddComponent<LocationCell>());
            _locationCellFactory.CreateNewLocationCell(locations, null).Returns(locationCells);

            locationCells[0].Initialize(locations[0]);
            _map.Add(locations);

            // Act.
            _map.Remove(locations);

            // Assert.
            _map.LocationCellsCount.Should().Be(0);
            _map.LocationsCount.Should().Be(0);
        }
    }
}
