using System.Linq;
using CVBuilder.Application.User.Responses;

namespace CVBuilder.Application.User.Mapper;

using Models;

public class UserMapper : AppMapperBase
{
    public UserMapper()
    {
        CreateMap<User, UserResult>()
            .ForMember(x => x.UserId, y => y.MapFrom(z => z.Id));

        CreateMap<User, SmallUserResult>()
            .ForMember(x => x.Role, y => y.MapFrom(z => z.Roles.FirstOrDefault()))
            .ForMember(x => x.CreatedAt, y => y.MapFrom(z => z.CreatedAt.ToString("MM/dd/yyyy HH:mm:ss UTC")));

        CreateMap<Models.Entities.Role, UserRoleResult>();
    }
}