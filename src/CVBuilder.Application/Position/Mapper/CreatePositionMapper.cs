using CVBuilder.Application.Position.Responses;

namespace CVBuilder.Application.Position.Mapper;

using Models.Entities;

public class CreatePositionMapper : AppMapperBase
{
    public CreatePositionMapper()
    {
        CreateMap<Position, PositionResult>()
            .ForMember(x => x.PositionId, y => y.MapFrom(z => z.Id));
    }
}