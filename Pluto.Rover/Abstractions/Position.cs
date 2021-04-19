using Pluto.Rover.Constants;

namespace Pluto.Rover.Abstractions
{
    public class Position
    {
        public Coordinate Coordinate { get; set; }
        public CardinalDirection Direction { get; set; }

        public Position(Coordinate coordinate, CardinalDirection direction)
        {
            Coordinate = coordinate;
            Direction = direction;
        }
        
        public override string ToString() => $"{Coordinate.Latitude}, {Coordinate.Longitude}, {Direction}";
    }
}