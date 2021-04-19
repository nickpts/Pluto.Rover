using System.Collections;
using System.Collections.Generic;
using Pluto.Rover.Abstractions;

namespace Pluto.Rover.Interfaces
{
    public interface IAstronomicalObject
    {
        int Width { get; }
        int Breadth { get; }
        ISet<Coordinate> Obstacles { get; }
    }
}