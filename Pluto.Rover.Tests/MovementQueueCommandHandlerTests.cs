using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

using Pluto.Rover.Abstractions;
using Pluto.Rover.Commands;
using Pluto.Rover.Constants;

namespace Pluto.Rover.Tests
{
    public class MovementQueueCommandHandlerTests
    {
        private Abstractions.Rover rover;
        private Planet pluto;
        
        [SetUp]
        public void Setup()
        {
            var initialPosition = new Position(new Coordinate(0, 0), CardinalDirection.North);
            pluto = new Planet(100, 100, new List<Coordinate>());
            rover = new Abstractions.Rover(initialPosition, pluto);
        }

        [TestCase(new[] {MotionCommand.F}, ExpectedResult = "0, 1, North")]
        [TestCase(new[]
        {
            MotionCommand.F,
            MotionCommand.F,
            MotionCommand.F,
            MotionCommand.F,
            MotionCommand.F
            
        }, ExpectedResult = "0, 5, North")]
        [TestCase(new[]
        {
            MotionCommand.F,
            MotionCommand.F,
            MotionCommand.F,
            MotionCommand.B,
            MotionCommand.B
            
        }, ExpectedResult = "0, 1, North")]
        public string MovementQueueHanlder_Moves_In_One_Axis_Successfully(MotionCommand[] queue)
        {
            return MovementQueueHandler_Test_Runner(queue);
        }

        [TestCase(new[] {MotionCommand.L}, ExpectedResult = "0, 0, West")]
        [TestCase(new[] {MotionCommand.R}, ExpectedResult = "0, 0, East")]
        public string MovementQueueHanlder_Rotates_Successfully(MotionCommand[] queue)
        {
            return MovementQueueHandler_Test_Runner(queue);
        }
        
        [TestCase(new []
        {
            MotionCommand.L,
        }, 0, 0, CardinalDirection.East, ExpectedResult = "0, 0, North")]
        [TestCase(new []
        {
            MotionCommand.R,
        }, 100, 100, CardinalDirection.West, ExpectedResult = "100, 100, North")]
        public string MovementQueueHandler_Rotation_Tests_With_Initial_Position(MotionCommand[] queue,
            int initialLatitude, int initialLongitude, CardinalDirection direction)
        {
            return MovementQueueHandler_With_InitialPosition_Test_Runner(queue, initialLatitude, initialLongitude, direction);
        }

        [TestCase(new[] {
            MotionCommand.F,
            MotionCommand.F,
            MotionCommand.L,
            MotionCommand.F
        }, ExpectedResult = "1, 2, West")]
        [TestCase(new[] {
            MotionCommand.F, 
            MotionCommand.F,
            MotionCommand.L,
            MotionCommand.L,
            MotionCommand.F,
            MotionCommand.R,
            MotionCommand.F
        }, ExpectedResult = "1, 1, West")]
        [TestCase(new[] {
            MotionCommand.F, 
            MotionCommand.F,
            MotionCommand.B,
            MotionCommand.L,
            MotionCommand.F,
            MotionCommand.F,
            MotionCommand.R,
            MotionCommand.F,
            MotionCommand.F,
            MotionCommand.R,
            MotionCommand.B,
        }, ExpectedResult = "3, 3, East")]
        public string MovementQueueHandler_Queue_With_Moves_And_Rotations(MotionCommand[] queue)
        {
            return MovementQueueHandler_Test_Runner(queue);
        }

        #region GridWrappingTests
        
        [TestCase(new []
        {
            MotionCommand.F
        }, 100, 100, CardinalDirection.North, ExpectedResult = "100, 0, North")]
        [TestCase(new []
        {
            MotionCommand.L,
            MotionCommand.F
        }, 100, 100, CardinalDirection.North, ExpectedResult = "0, 100, West")]
        [TestCase(new []
        {
            MotionCommand.B
        }, 0, 0, CardinalDirection.North, ExpectedResult = "0, 100, North")]
        [TestCase(new []
        {
            MotionCommand.L,
            MotionCommand.B,
        }, 0, 0, CardinalDirection.North, ExpectedResult = "100, 0, West")]
        public string MovementQueueHandler_Wraps_Works_Appropriately(MotionCommand[] queue,
            int initialLatitude, int initialLongitude, CardinalDirection direction)
        {
            return MovementQueueHandler_With_InitialPosition_Test_Runner(queue, initialLatitude, initialLongitude, direction);
        }


        [Test]
        public void MovementQueueHandler_Obstacle_Encountered_When_Moving_Forward_Rover_Stays_Put()
        {
            var initialPosition = new Position(new Coordinate(0, 0), CardinalDirection.North);
            var pluto = new Planet(100, 100, new List<Coordinate>() { new(0, 1) });
            var rover = new Abstractions.Rover(initialPosition, pluto);
            
            var commandQueue = new MovementQueueCommand()
            {
                Vehicle = rover,
                MotionCommandQueue = new Queue<MotionCommand>(new []
                {
                    MotionCommand.F
                })
            };

            var result = new MovementCommandHandler().Handle(commandQueue, default).Result;
                        
            result.Position.ToString().Should().Be("0, 0, North");
            result.FailureReason.Should().Be(FailureReason.ObstacleDectected);
        }
        
        [Test]
        public void MovementQueueHandler_Obstacle_Encountered_When_Moving_Backward_Rover_Stays_Put()
        {
            var initialPosition = new Position(new Coordinate(1, 1), CardinalDirection.North);
            var pluto = new Planet(100, 100, new List<Coordinate>() { new(1, 0) });
            var rover = new Abstractions.Rover(initialPosition, pluto);
            
            var commandQueue = new MovementQueueCommand()
            {
                Vehicle = rover,
                MotionCommandQueue = new Queue<MotionCommand>(new []
                {
                    MotionCommand.B
                })
            };

            var result = new MovementCommandHandler().Handle(commandQueue, default).Result;
                        
            result.Position.ToString().Should().Be("1, 1, North");
            result.FailureReason.Should().Be(FailureReason.ObstacleDectected);
        }
        
        #endregion

        #region Private implementation
        private string MovementQueueHandler_Test_Runner(MotionCommand[] queue)
        {
            var commandQueue = new MovementQueueCommand()
            {
                Vehicle = rover,
                MotionCommandQueue = new Queue<MotionCommand>(queue)
            };

            var newPosition = new MovementCommandHandler().Handle(commandQueue, default).Result.Position;

            return newPosition.ToString();
        }

        private string MovementQueueHandler_With_InitialPosition_Test_Runner(MotionCommand[] queue, int initialLatitude,
            int initialLongitude, CardinalDirection direction)
        {
            var initialPosition = new Position(new Coordinate(initialLatitude, initialLongitude), direction);
            var rover = new Abstractions.Rover(initialPosition, pluto);
            
            var commandQueue = new MovementQueueCommand()
            {
                Vehicle = rover,
                MotionCommandQueue = new Queue<MotionCommand>(queue)
            };
            
            var newPosition = new MovementCommandHandler().Handle(commandQueue, default).Result.Position;

            return newPosition.ToString();
        }
        
        #endregion
    }
}