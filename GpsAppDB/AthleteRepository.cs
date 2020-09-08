using System.Linq;

namespace GpsAppDB
{
    public class AthleteRepository
    {
        private readonly AthleteContext _context;

        public AthleteRepository(AthleteContext context)
        {
            _context = context;
        }

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