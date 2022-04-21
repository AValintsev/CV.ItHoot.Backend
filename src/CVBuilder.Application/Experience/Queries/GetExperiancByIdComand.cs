using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVBuilder.Application.Expiriance.Respons;
using MediatR;

namespace CVBuilder.Application.Expiriance.Queries
{
    public class GetExperiancByIdComand : MediatR.IRequest<GetExpirianceByIdResult>
    {
        public int Id { get; set; } 
    }
}
