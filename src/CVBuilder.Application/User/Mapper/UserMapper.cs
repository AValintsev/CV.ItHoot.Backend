using CVBuilder.Application.User.Responses;

namespace CVBuilder.Application.User.Mapper;
using Models;
public class UserMapper:AppMapperBase
{
    public UserMapper()
    {
        CreateMap<User, UserResult>()
            .ForMember(x=>x.UserId,y=>y.MapFrom(z=>z.Id));
    }
}