using GpsAppDB;
using System.Runtime.Serialization;

namespace Controllers.Controllers.Responses
{
    [DataContract]
    public class GeoLocation : NetTopologySuite.Geometries.Point
    {
        public GeoLocation(double latitude, double longitude)
            : base(longitude, latitude) =>
            SRID = ExploViewer.SRID;

        [DataMember]
        public double Longitude => X;

        [DataMember]
        public double Latitude => Y;
    }
}