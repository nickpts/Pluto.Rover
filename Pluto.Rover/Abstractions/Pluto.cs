using System;
using System.Collections;
using System.Collections.Generic;
using Pluto.Rover.Interfaces;

namespace Pluto.Rover.Abstractions
{
    public class Planet: IAstronomicalObject
    {
        public int Width { get; }
        public int Breadth { get; }
        public ISet<Coordinate> Obstacles { get; }

        public Planet(int width, int breadth, IEnumerable<Coordinate> obstacles)
        {
            if (obstacles == null)
                throw new ArgumentNullException(nameof(obstacles));

            Width = width;
            Breadth = breadth;
            //hashset for faster lookups
            Obstacles = new HashSet<Coordinate>(obstacles);
        }
    }
}