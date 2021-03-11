using System.Runtime.Serialization;

namespace GpsAppDB.Entities
{
    [DataContract]
    public class GeoLocation : NetTopologySuite.Geometries.Point
    {
        public GeoLocation(double longitude, double latitude)
            : base(longitude, latitude) =>
            SRID = ExploViewer.SRID;

        [DataMember]
        public double Longitude => X;

        [DataMember]
        public double Latitude => Y;
    }
}