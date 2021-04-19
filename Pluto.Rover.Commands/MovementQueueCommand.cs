using System;
using System.Collections.Generic;
using MediatR;
using Pluto.Rover.Abstractions;
using Pluto.Rover.Constants;
using Pluto.Rover.Interfaces;

namespace Pluto.Rover.Commands
{
    public class MovementResult
    {
        public Position Position { get; set; }
        public FailureReason FailureReason { get; set; }
    }
    
    public class MovementQueueCommand: IRequest<MovementResult>
    {
        public IVehicle Vehicle { get; set; }
        public Queue<MotionCommand> MotionCommandQueue { get; set; }
    }
}