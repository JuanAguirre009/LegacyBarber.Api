namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Credentials required to authenticate a user.
    /// </summary>
    public class LoginModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
