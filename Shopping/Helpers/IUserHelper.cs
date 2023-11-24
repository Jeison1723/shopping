using Microsoft.AspNetCore.Identity;
using Shopping.Controllers.Data.Entities;
using Shopping.Models;

namespace Shopping.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);


        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<User> AddUserAsync(AddUserViewModel modelo);
        Task CheckRoleAsync(string roleName);


        Task AddUserToRoleAsync(User user, string roleName);


        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
