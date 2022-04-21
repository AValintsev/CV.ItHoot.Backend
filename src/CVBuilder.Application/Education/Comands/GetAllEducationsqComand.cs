using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVBuilder.Application.CV.Responses.CvResponses;
using MediatR;

namespace CVBuilder.Application.Education.Queries
{
    public class GetAllEducationsqComand : MediatR.IRequest<List<EducationResult>>
    {

    }
}
