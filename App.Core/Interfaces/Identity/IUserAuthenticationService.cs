namespace LegacyBarber.App.Core.Interfaces.Identity
{
    /// <summary>
    /// Provides access to the authenticated user's identity information.
    /// </summary>
    public interface IUserAuthenticationService
    {
        /// <summary>Gets the short identifier (user name) of the current authenticated user.</summary>
        string UserName { get; }

        /// <summary>Gets the descriptive or display name of the current authenticated user.</summary>
        string DescriptiveUserName { get; }
    }
}
