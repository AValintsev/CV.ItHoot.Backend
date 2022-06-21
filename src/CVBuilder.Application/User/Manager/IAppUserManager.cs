using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Application.User.Manager
{
    public interface IAppUserManager
    {
        Task<IdentityResult> CreateAsync(Models.User user, string password);
        Task<IdentityResult> AddToRoleAsync(Models.User user, string role);
        Task<IdentityResult> AddToRolesAsync(Models.User user, IEnumerable<string> roles);

        Task<Models.User> FindByEmailAsync(string email);
        Task<Models.User> FindByPhoneAsync(string phone);

        Task<Models.User> FindByIdAsync(string userId);
        Task<Models.User> FindByIdAsync(int userId);


        Task<string> GeneratePasswordResetTokenAsync(Models.User user);
        Task<IdentityResult> ResetPasswordAsync(Models.User user, string token, string newPassword);

        Task<IdentityResult> ChangePasswordAsync(Models.User user, string currentPassword, string newPassword);
        Task<bool> CheckPasswordAsync(Models.User user, string password);

        Task<IdentityResult> SetEmailAsync(Models.User user, string email);
        Task<IdentityResult> ConfirmEmailAsync(Models.User user, string token);

        Task<IdentityResult> ChangePhoneNumberAsync(Models.User user, string phoneNumber, string token);

        Task<string> GenerateEmailConfirmationTokenAsync(Models.User user);
        Task<string> GenerateChangePhoneNumberTokenAsync(Models.User user, string phoneNumber);

        Task<IList<Claim>> GetClaimsAsync(Models.User user);
        Task<IList<string>> GetRolesAsync(Models.User user);
    }
}