using LegacyBarber.App.Core.Interfaces.Identity;
using System.Security.Claims;

namespace LegacyBarber.App.Api.Utils
{
    /// <summary>
    /// Provides identity information of the current HTTP user for audit purposes.
    /// </summary>
    public sealed class UserServices : IUserAuthenticationService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserServices(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string UserName
        {
            get
            {
                ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;
                if (user?.Identity?.IsAuthenticated == true)
                    return user.Identity.Name ?? "UNKNOWN";

                return "Anonymous";
            }
        }

        public string DescriptiveUserName => UserName;
    }
}
