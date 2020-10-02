using System.Net.Http;
using System.Threading.Tasks;

namespace Controllers.UserManagement
{
    public interface IAuthenticatedAthleteFactory
    {
        public Task<AuthenticatedAthlete> Create(HttpResponseMessage response);
    }
}