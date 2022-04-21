using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.User.Queries;
using CVBuilder.Application.User.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.User.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
    {
        private readonly IRepository<Models.Entities.User, int> _repository;

        public GetAllUsersHandler(
            IRepository<Models.Entities.User, int> repository)
        {
            _repository = repository;
        }

        public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetListAsync(x => true);

            throw new NotImplementedException();

            // todo add mapper
            //var result = list.Adapt<List<UserResponse>>();
            //return result;
        }
    }
}