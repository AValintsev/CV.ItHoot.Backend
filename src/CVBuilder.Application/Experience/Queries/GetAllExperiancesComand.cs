using System.Collections.Generic;
using CVBuilder.Application.Expiriance.Respons;
using MediatR;

namespace CVBuilder.Application.Expiriance.Queries
{
    public class GetAllExperiancesComand : IRequest<List<ExperianceResult>>
    {

    }
}
