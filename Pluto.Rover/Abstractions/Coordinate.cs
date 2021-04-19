using System;

namespace Pluto.Rover.Abstractions
{
    public struct Coordinate
    {
        // double precision floating-point for geocoordinates 
        private readonly double latitude;
        private readonly double longitude;

        public double Latitude => latitude;
        public double Longitude => longitude;
        
        public Coordinate(double latitude, double longitude)
        {
            // NB. we do defensive checking here because the spec 
            // specifies only positive attitudes for coordinates
            if (latitude < 0)
                throw new ArgumentException(nameof(latitude));
            
            if (longitude < 0)
                throw new ArgumentException(nameof(longitude));
                
            this.latitude = latitude;
            this.longitude = longitude;
        }
        
        public override string ToString()
        {
            return $"{Latitude},{Longitude}";
        }
    }
}