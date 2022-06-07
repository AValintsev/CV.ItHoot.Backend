﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CVBuilder.Web.Infrastructure.BaseControllers
{
    [Authorize]
    public class BaseAuthApiController : BaseApiController
    {
        private int? _loggedUserId;

        protected int? LoggedUserId
        {
            get
            {
                if (_loggedUserId != null)
                {
                    return _loggedUserId.Value;
                }

                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                // if (userId == null)
                // {
                //     throw new KeyNotFoundException("UserId not in claims");
                // }

                var isSuccess = int.TryParse(userId?.Value, out var id);
                if (isSuccess)
                {
                    _loggedUserId = id;
                }
                return _loggedUserId;
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
