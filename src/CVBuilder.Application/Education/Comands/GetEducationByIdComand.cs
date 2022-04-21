using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVBuilder.Application.Education.Response;
using MediatR;
namespace CVBuilder.Application.Education.Comands
{
    public class GetEducationByIdComand : MediatR.IRequest<EducationByIdResult>
    {
        public int  Id { get; set; }
    }
}
