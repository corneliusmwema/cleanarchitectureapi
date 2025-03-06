using Microsoft.AspNetCore.Authorization;

namespace API.Authorization
{
    public static class Policies
    {
        public const string Admin = "AdminPolicy";

        public const string User = "UserPolicy";

        public const string AuthorizedUser = "AuthorizedUser";


        public static void AddPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(Admin, policy => policy.RequireRole("Admin"));

            options.AddPolicy(User, policy => policy.RequireRole("User"));

            options.AddPolicy(AuthorizedUser, policy => policy.RequireAuthenticatedUser());
        }
    }
};