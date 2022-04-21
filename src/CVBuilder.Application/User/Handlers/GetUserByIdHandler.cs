using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Caching.Interfaces;
using CVBuilder.Application.Helpers;
using CVBuilder.Application.User.Queries;
using CVBuilder.Application.User.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.User.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly ICacheKeyService _cacheKeyService;

        private readonly IRepository<Models.Entities.User, int> _repository;
        private readonly IStaticCacheManager _staticCacheManager;

        public GetUserByIdHandler(
            IRepository<Models.Entities.User, int> repository,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager)
        {
            _repository = repository;
            _cacheKeyService = cacheKeyService;
            _staticCacheManager = staticCacheManager;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(
                UserDefaults.UserByIdPrefixCacheKey,
                request.Id);

            var result = await _staticCacheManager.GetAsync(key, async () =>
            {
                var user = await _repository.GetByIdAsync(request.Id);

                var response = new UserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                };

                return response;
            });

            return result;
        }
    }
}