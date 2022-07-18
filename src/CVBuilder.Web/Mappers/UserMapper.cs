using CVBuilder.Application.User.Queries;
using CVBuilder.Web.Contracts.V1.Requests.User;

namespace CVBuilder.Web.Mappers;

public class UserMapper:MapperBase
{
    public UserMapper()
    {
        CreateMap<GetAllUsersRequest, GetAllUsersQuery>();
    }
}