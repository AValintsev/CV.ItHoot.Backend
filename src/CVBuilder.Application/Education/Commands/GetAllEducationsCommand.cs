using System.Collections.Generic;
using CVBuilder.Application.CV.Responses.CvResponses;
using MediatR;

namespace CVBuilder.Application.Education.Commands
{
    public class GetAllEducationsCommand : IRequest<List<EducationResult>>
    {

    }
}
