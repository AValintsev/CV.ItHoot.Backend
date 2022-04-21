using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CVBuilder.Web.Infrastructure.BaseControllers
{
    [Authorize]
    public class BaseAuthApiController : BaseApiController
    {
        private int? _loggedUserId;

        protected int LoggedUserId
        {
            get
            {
                if (_loggedUserId != null)
                {
                    return _loggedUserId.Value;
                }

                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new KeyNotFoundException("UserId not in claims");
                }

                _loggedUserId = int.Parse(userId.Value);
                return _loggedUserId.Value;
            }
        }

        protected IEnumerable<string> LoggedUserRoles
        {
            get
            {
                var userRoles = User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);

                return userRoles;
            }
        }
    }
}
