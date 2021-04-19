using System.Collections.Generic;
using Pluto.Rover.Abstractions;
using Pluto.Rover.Constants;

namespace Pluto.Rover.Interfaces
{
    public interface IVehicle
    {
        Position CurrentPosition { get; }
        Position PreviousPosition { get; }
        bool CollisionDetected { get; }
        void Move(MotionCommand command);
    }
}