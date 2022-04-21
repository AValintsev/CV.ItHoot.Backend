using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Caching.Interfaces;
using CVBuilder.Application.Helpers;
using CVBuilder.Application.User.Notifications;
using MediatR;

namespace CVBuilder.Application.User.NotificationHandlers
{
    public class UserClearCacheNotificationHandler : INotificationHandler<UserClearCacheNotification>
    {
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;

        public UserClearCacheNotificationHandler(
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager)
        {
            _cacheKeyService = cacheKeyService;
            _staticCacheManager = staticCacheManager;
        }

        public Task Handle(UserClearCacheNotification notification, CancellationToken cancellationToken)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(
                UserDefaults.UserByIdPrefixCacheKey,
                notification.Id);

            if (notification.User != null)
            {
                _staticCacheManager.Set(key, notification.User);
            }
            else
            {
                if (_staticCacheManager.IsSet(key)) _staticCacheManager.Remove(key);
            }

            return Task.CompletedTask;
        }
    }
}