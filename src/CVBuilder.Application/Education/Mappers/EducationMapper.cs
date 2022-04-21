using CVBuilder.Application.Education.Comands;
using CVBuilder.Application.Education.Queries;
using CVBuilder.Application.Education.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.Education.Mappers
{
    internal class EducationMapper : AppMapperBase
    {
        public EducationMapper()
        {
            CreateMap<CVBuilder.Models.Entities.Education, CreateEducationComand>().ReverseMap();

            //CreateMap<CVBuilder.Models.Entities.Education, GetEducationByIdComand>().ReverseMap();
            CreateMap<CVBuilder.Models.Entities.Education, EducationByIdResult>().ReverseMap();
            CreateMap<CVBuilder.Models.Entities.Education, CreateEducationResult>();
            
        }
    }
}
