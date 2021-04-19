using System.Collections.Generic;
using MediatR;
using Pluto.Rover.Interfaces;

namespace Pluto.Rover.Commands
{
    public class ChangeDirectionCommandQueue: ICommand, IRequest
    {
        public Queue<MotionCommand> MotionCommand { get; set; }
    }
}