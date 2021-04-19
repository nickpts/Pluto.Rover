using System;
using System.Collections.Generic;
using NUnit.Framework;
using Pluto.Rover.Abstractions;
using Pluto.Rover.Constants;

namespace Pluto.Rover.Tests
{
    public class PositionTests
    {
        [Test]
        public void Coordinate_Throws_ArgumentException_For_Negative_Latitude() =>
            Assert.Throws<ArgumentException>(() => new Coordinate(-1, 0));
        
        [Test]
        public void Coordinate_Throws_ArgumentException_For_Negative_Longitude() =>
            Assert.Throws<ArgumentException>(() => new Coordinate(0, -1));
        
    }
}