using CVBuilder.Application.Identity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.Identity.Commands
{
    public class GetCurrentUserByTokenCommand : IRequest<AuthenticationResult>
    {
        public int UserId { get; set; }
    }
}
