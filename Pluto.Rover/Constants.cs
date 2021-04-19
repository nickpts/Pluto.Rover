using System;

namespace Pluto.Rover.Constants
{
    public enum CardinalDirection
    {
        North = 0,
        West = 1,
        South = 2,
        East = 3
    }

    public enum MotionCommand
    {
        F,
        B,
        L,
        R
    }

    public enum FailureReason
    {
        ObstacleDectected
    }
}