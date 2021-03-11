using GpsAppDB.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GpsAppDB.Repositories
{
    public class MarkerRepository : IMarkerRepository
    {
        private readonly ExploViewer _context;
        private readonly IAthleteRepository _athleteRepository;

        public MarkerRepository(ExploViewer context, IAthleteRepository athleteRepository)
        {
            _context = context;
            _athleteRepository = athleteRepository;
        }

        public Marker Add(string athleteId, string name, double latitude, double longitude)
        {
            var athlete = _athleteRepository.Get(athleteId);

            var marker = new Marker
            {
                Athlete = athlete,
                Name = name,
                Latitude = latitude,
                Longitude = longitude,
                Radius = 500
            };

            _context.Markers.Add(marker);
            _context.SaveChanges();

            return marker;
        }

        public void Delete(string athleteId, int id)
        {
            var marker =_context.Markers.FirstOrDefault(m => m.Id == id);
            if (marker != null) _context.Remove(marker);
            _context.SaveChanges();
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
