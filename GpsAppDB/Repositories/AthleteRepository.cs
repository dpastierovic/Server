using GpsAppDB.Entities;
using System.Linq;

namespace GpsAppDB.Repositories
{
    public class AthleteRepository : IAthleteRepository
    {
        private readonly ExploViewer _context;

        public AthleteRepository(ExploViewer context)
        {
            _context = context;
        }

        public Athlete Get(string athleteId) =>
            _context.Athletes.FirstOrDefault(athlete => athlete.AthleteId == athleteId);

        public Athlete Add(string athleteId, string firstName, string lastName)
        {
            var athlete = new Athlete
            {
                AthleteId = athleteId,
                FirstName = firstName,
                LastName = lastName
            };

            _context.Athletes.Add(athlete);
            _context.SaveChanges();

            return athlete;
        }

        public bool IsStored(string athleteId)
        {
            return _context.Athletes.Any(athlete => athlete.AthleteId == athleteId);
        }
    }
}