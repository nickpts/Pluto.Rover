using System;
using Pluto.Rover.Constants;

namespace Pluto.Rover.Abstractions
{
    public abstract class DomainEvent
    {
        protected DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;
    }

    public class SuccessfulMovementEvent: DomainEvent
    {
        public Position previousPosition { get; set; }
        public Position newPosition { get; set; }
    }

    public class FailedMovementEvent : DomainEvent
    {
        public Position currentPosition { get; set; }
        public FailureReason failureReason { get; set; }
    }
}