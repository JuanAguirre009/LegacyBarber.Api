namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Request to exchange a refresh token for a new access token.
    /// </summary>
    public class RefreshTokenRequestModel
    {
        public string TokenRefresco { get; set; } = string.Empty;
    }
}
