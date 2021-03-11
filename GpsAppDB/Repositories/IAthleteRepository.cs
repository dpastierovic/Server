using GpsAppDB.Entities;

namespace GpsAppDB.Repositories
{
    public interface IAthleteRepository
    {
        public Athlete Get(string athleteId);

        public Athlete Add(string athleteId, string firstName, string lastName);

        public bool IsStored(string athleteId);
    }
}