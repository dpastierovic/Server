using GpsAppDB.Entities;
using System.Collections.Generic;

namespace GpsAppDB.Repositories
{
    public interface IMarkerRepository
    {
        Marker Add(string name, double latitude, double longitude);

        IEnumerable<Marker> GetAll(Athlete athlete);

        IEnumerable<Marker> GetAll(string athleteId);
    }
}