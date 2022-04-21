using CVBuilder.Application.Expiriance.Queries;
using CVBuilder.Application.Expiriance.Respons;
using CVBuilder.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.Expiriance.Mapper
{
    public class ExperienceMapper : AppMapperBase
    {
        public ExperienceMapper()
        {
            CreateMap<CreateExperiencComand, Experience>();

            CreateMap<Experience, ExperianceResult>();

            CreateMap<Experience, GetExpirianceByIdResult>().ReverseMap();

            CreateMap<Exception, CreateExpirienceResult>();

        }

        
    }
}
