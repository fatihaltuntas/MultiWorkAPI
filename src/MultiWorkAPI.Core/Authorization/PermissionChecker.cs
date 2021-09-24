using Abp.Authorization;
using MultiWorkAPI.Authorization.Roles;
using MultiWorkAPI.Authorization.Users;

namespace MultiWorkAPI.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
