using System;
using System.Collections.Generic;
using Pluto.Rover.Constants;
using Pluto.Rover.Interfaces;

namespace Pluto.Rover.Abstractions
{
    public class Rover: IVehicle
    {
        private Position currentPosition;
        private Position previousPosition;
        private bool collisionDetected;
        private readonly IAstronomicalObject astronomicalObject;

        /// <param name="initialPosition">The position the rover has landed at</param>
        /// <param name="astronomicalObject">The body on which the rover has landed</param>
        public Rover(Position landingPosition, IAstronomicalObject astronomicalObject)
        {
            currentPosition = landingPosition;
            this.astronomicalObject = astronomicalObject;
            
            previousPosition = default;
        }

        public Position CurrentPosition => currentPosition;
        public Position PreviousPosition => previousPosition;
        public bool CollisionDetected => collisionDetected;

        public void Move(MotionCommand command)
        {
            collisionDetected = false;
            
            switch (command)
            {
                case MotionCommand.F:
                    collisionDetected = MoveForward();        
                    break;
                case MotionCommand.B:
                    collisionDetected = MoveBackward();
                    break;
                case MotionCommand.L:
                    TurnLeft90Degrees();
                    break;
                case MotionCommand.R:
                    TurnRight90Degrees();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(command), command, null);
            }
        }

        /// <summary>
        /// The rover moves back by one grid, maintaining the same direction
        /// Depending by the direction it faces it may move on the latitude or longitude axis
        /// </summary>
        private bool MoveForward()
        {
            var newLatitude = currentPosition.Coordinate.Latitude;
            var newLongitude = currentPosition.Coordinate.Longitude;
            
            switch (currentPosition.Direction)
            {
                case CardinalDirection.North:
                    newLongitude = IncrementLongitudeWithWrapping();     
                    break;
                case CardinalDirection.West:
                    newLatitude = IncrementLatitudeWithWrapping();
                    break;
                case CardinalDirection.South:
                    newLongitude = DecrementLongitudeWithWrapping();
                    break;
                case CardinalDirection.East:
                    newLatitude = DecrementLatitudeWithWrapping();
                    break;
                default:
                    newLatitude = currentPosition.Coordinate.Latitude;
                    newLongitude = currentPosition.Coordinate.Longitude;
                    break;
            }

            var newPosition = new Position(new Coordinate(newLatitude, newLongitude), currentPosition.Direction);

            if (astronomicalObject.Obstacles.Contains(newPosition.Coordinate))
                return true;
            
            previousPosition = currentPosition;
            currentPosition = newPosition;

            return false;
        }

        /// <summary>
        /// The rover moves back by one grid, maintaining the same direction
        /// Depending by the direction it faces it may move on the latitude or longitude axis
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private bool MoveBackward()
        {
            var newLatitude = currentPosition.Coordinate.Latitude;
            var newLongitude = currentPosition.Coordinate.Longitude;
            
            switch (currentPosition.Direction)
            {
                case CardinalDirection.North:
                    newLongitude = DecrementLongitudeWithWrapping();
                    break;
                case CardinalDirection.West:
                    newLatitude = DecrementLatitudeWithWrapping();
                    break;
                case CardinalDirection.South:
                    newLongitude = IncrementLongitudeWithWrapping();  
                    break;
                case CardinalDirection.East:
                    newLatitude = IncrementLatitudeWithWrapping();
                    break;
                default:
                    newLatitude = currentPosition.Coordinate.Latitude;
                    newLongitude = currentPosition.Coordinate.Longitude;
                    break;
            }

            var newPosition = new Position(new Coordinate(newLatitude, newLongitude), currentPosition.Direction);

            if (astronomicalObject.Obstacles.Contains(newPosition.Coordinate))
                return true;

            previousPosition = currentPosition;
            currentPosition = newPosition;

            return false;
        }

        /// <summary>
        /// The rover rotates 90 degrees counter-clockwise, changing direction
        /// longitude/latitude remain the same
        /// </summary>
        /// <example>CardinalDirection.North -> CardinalDirection.West</example>
        private void TurnLeft90Degrees()
        {
            var newDirection = (int)currentPosition.Direction + 1 > 3 ? 0 : (int) currentPosition.Direction + 1;

            currentPosition = new Position(
                new Coordinate(currentPosition.Coordinate.Latitude,
                    currentPosition.Coordinate.Longitude),
                (CardinalDirection)newDirection);
        }

        /// <summary>
        /// The rover rotates 90 degrees clockwise, changing direction
        /// longitude/latitude remain the same
        /// </summary>
        /// <example>CardinalDirection.North -> CardinalDirection.EAST</example>
        private void TurnRight90Degrees()
        {
            var newDirection = (int) currentPosition.Direction - 1 < 0 ? 3 : (int) currentPosition.Direction - 1;
            
            currentPosition = new Position(
                new Coordinate(currentPosition.Coordinate.Latitude,
                    currentPosition.Coordinate.Longitude),
                (CardinalDirection)newDirection);
        }

        
        private double IncrementLongitudeWithWrapping() =>
            currentPosition.Coordinate.Longitude + 1 > astronomicalObject.Width
                ? 0
                : currentPosition.Coordinate.Longitude + 1;

        private double DecrementLongitudeWithWrapping() =>
            currentPosition.Coordinate.Longitude - 1 < 0
                ? astronomicalObject.Width
                : currentPosition.Coordinate.Longitude - 1;

        private double IncrementLatitudeWithWrapping() =>
            currentPosition.Coordinate.Latitude + 1 > astronomicalObject.Breadth
                ? 0
                : currentPosition.Coordinate.Latitude + 1;
        
        private double DecrementLatitudeWithWrapping() => 
            currentPosition.Coordinate.Latitude - 1 < 0
            ? astronomicalObject.Breadth
            : currentPosition.Coordinate.Latitude - 1;
    }
}