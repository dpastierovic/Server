using NetTopologySuite.Geometries;
using System;

namespace Controllers.Utilities
{
    public static class GeometryFactoryExtensions
    {
        /// <summary>
        /// Creates polygon representing circle around point with given radius.
        /// </summary>
        /// <param name="longitude">Longitude of the center point.</param>
        /// <param name="latitude">Latitude of the center point.</param>
        /// <param name="radius">Radius in meters.</param>
        public static Geometry CreateCircle(this GeometryFactory factory, double longitude, double latitude, double radius)
        {
            if (factory.SRID != 4326) throw new InvalidOperationException($"SRID {factory.SRID} is unsupported. Only SRID=4326 is supported");
            var point = new Point(longitude, latitude).Buffer(Math.Cos(latitude) * 0.111 * radius);
            return point;
        }
    }
}
