using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Pluto.Rover.Abstractions;
using Pluto.Rover.Constants;

namespace Pluto.Rover.Tests
{
    public class RoverTests
    {
        [Test]
        public void Rover_MotionCommand_Enum_Throws_Argument_Out_Of_Range_Exception()
        {
            var pluto = new Planet(100, 100, new List<Coordinate>());
            var rover = new Abstractions.Rover(new Position(new Coordinate(0, 0), CardinalDirection.North), pluto);

            Assert.Throws<ArgumentOutOfRangeException>(() => rover.Move((MotionCommand) 5));
        }
    }
}