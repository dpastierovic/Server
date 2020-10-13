using GpsAppDB.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GpsAppDB.Repositories
{
    public class MarkerRepository : IMarkerRepository
    {
        private readonly ExploViewer _context;

        public MarkerRepository(ExploViewer context)
        {
            _context = context;
        }

        public Marker Add(string name, double latitude, double longitude)
        {
            var marker = new Marker
            {
                Name = name,
                Latitude = latitude,
                Longitude = longitude
            };

            _context.Markers.Add(marker);
            _context.SaveChanges();

            return marker;
        }

        public IEnumerable<Marker> GetAll(Athlete athlete)
        {
            return GetAll(athlete.AthleteId);
        }

        public IEnumerable<Marker> GetAll(string athleteId)
        {
            var markers = _context.Markers
                .Where(marker => marker.Athlete.AthleteId == athleteId)
                .ToList();

            foreach (var marker in markers)
            {
                _context.Entry(marker).Reference(c => c.Athlete).Load();
            }

            return markers;
        }
    }
}
