using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Resume.Mapper;

public class TemplateMapper : AppMapperBase
{
    public TemplateMapper()
    {
        CreateMap<ResumeTemplate, TemplateResult>()
            .ForMember(x => x.TemplateId, y => y.MapFrom(z => z.Id));
    }
}