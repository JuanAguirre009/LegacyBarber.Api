namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Represents the result of a successful authentication.
    /// </summary>
    public class TokenPairModel
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
