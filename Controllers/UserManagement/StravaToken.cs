using System;

namespace Controllers.UserManagement
{
    public class StravaToken
    {
        public StravaToken(string athleteId, string accessToken, string refreshToken, DateTimeOffset expiresAt)
        {
            AthleteId = athleteId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresAt = expiresAt;
        }

        public string AthleteId { get; }

        public string AccessToken { get; }

        public string RefreshToken { get; }

        public DateTimeOffset ExpiresAt { get; }

        public TimeSpan ExpiresIn => ExpiresAt - DateTimeOffset.Now;
    }
}