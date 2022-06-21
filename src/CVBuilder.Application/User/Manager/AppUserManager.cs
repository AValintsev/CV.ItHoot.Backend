using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CVBuilder.Application.User.Manager
{
    public class AppUserManager : UserManager<Models.User>, IAppUserManager
    {
        public AppUserManager(
            IUserStore<Models.User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<Models.User> passwordHasher,
            IEnumerable<IUserValidator<Models.User>> userValidators,
            IEnumerable<IPasswordValidator<Models.User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<Models.User>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                services, logger)
        {
        }

        public async Task<Models.User> FindByPhoneAsync(string phone) =>
            await Users.FirstOrDefaultAsync(r => r.PhoneNumber == phone);

        public async Task<Models.User> FindByIdAsync(int userId) =>
            await Users.FirstOrDefaultAsync(r => r.Id == userId);
    }
}