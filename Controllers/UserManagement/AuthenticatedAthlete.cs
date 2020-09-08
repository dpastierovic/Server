using System;

namespace Controllers.UserManagement
{
    public class AuthenticatedAthlete
    {
        public AuthenticatedAthlete(string athleteId,
            string accessToken,
            string refreshToken,
            DateTimeOffset expiresAt,
            string firstName,
            string lastName)
        {
            AthleteId = athleteId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresAt = expiresAt;
            FirstName = firstName;
            LastName = lastName;
        }

        public string AthleteId { get; }

        public string AccessToken { get; }

        public string RefreshToken { get; }

        public DateTimeOffset ExpiresAt { get; }

        public string FirstName { get; }

        public string LastName { get; }
    }
}