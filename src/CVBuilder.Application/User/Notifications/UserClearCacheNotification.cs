using CVBuilder.Application.User.Responses;
using MediatR;

namespace CVBuilder.Application.User.Notifications
{
    public class UserClearCacheNotification : INotification
    {
        public UserClearCacheNotification(int id, UserResponse user = null)
        {
            Id = id;
            User = user;
        }

        public int Id { get; }
        public UserResponse User { get; }
    }
}