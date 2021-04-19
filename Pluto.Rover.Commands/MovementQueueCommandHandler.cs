using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pluto.Rover.Abstractions;
using Pluto.Rover.Constants;

namespace Pluto.Rover.Commands
{
    public class MovementCommandHandler : IRequestHandler<MovementQueueCommand, MovementResult>
    {
        public Task<MovementResult> Handle(MovementQueueCommand request, CancellationToken cancellationToken)
        {
            var vehicle = request.Vehicle;
            MovementResult result = new MovementResult();

            foreach (var command in request.MotionCommandQueue)
            {
                vehicle.Move(command);

                if (vehicle.CollisionDetected)
                {
                    result = new MovementResult()
                    {
                        Position = request.Vehicle.CurrentPosition,
                        FailureReason = FailureReason.ObstacleDectected
                    };
                    // can be routed to subscribers
                    new FailedMovementEvent()
                    {
                        currentPosition = request.Vehicle.CurrentPosition,
                        failureReason = FailureReason.ObstacleDectected
                    };

                    break;
                }
                
                result = new MovementResult() {Position = request.Vehicle.CurrentPosition};

                // can be routed to subscribers
                new SuccessfulMovementEvent()
                {
                    previousPosition = request.Vehicle.PreviousPosition,
                    newPosition = request.Vehicle.CurrentPosition,
                };
            }

            // could do more stuff here like returning a JourneySuccessfulEvent
            // we would need a journey abstraction with startPosition and endPosition
            return Task.FromResult(result);
        }
    }
}