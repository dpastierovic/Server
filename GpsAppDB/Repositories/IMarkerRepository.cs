using GpsAppDB.Entities;
using System.Collections.Generic;

namespace GpsAppDB.Repositories
{
    public interface IMarkerRepository
    {
        Marker Add(string athleteId, string name, double latitude, double longitude);

        void Delete(string athleteId, int id);

        IEnumerable<Marker> GetAll(Athlete athlete);

        IEnumerable<Marker> GetAll(string athleteId);
    }
}