using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Controllers.UserManagement
{
    public class AuthenticatedAthleteFactory : IAuthenticatedAthleteFactory
    {
        public async Task<AuthenticatedAthlete> Create(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var contentDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

            if (!contentDictionary.ContainsKey("access_token") ||
                !contentDictionary.ContainsKey("refresh_token") ||
                !contentDictionary.ContainsKey("expires_at") ||
                !contentDictionary.ContainsKey("athlete"))
            {
                return null;
            }

            var athlete = (JObject)contentDictionary["athlete"];

            return new AuthenticatedAthlete((string)athlete["id"],
                (string)contentDictionary["access_token"],
                (string)contentDictionary["refresh_token"],
                DateTimeOffset.FromUnixTimeSeconds((long)contentDictionary["expires_at"]),
                (string)athlete["firstname"],
                (string)athlete["lastname"],
                (string)athlete["profile_medium"]);
        }
    }
}