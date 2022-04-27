using System;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.User.Commands;
using CVBuilder.Application.User.Notifications;
using CVBuilder.Application.User.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.User.Handlers
{
    public class EditUserHandler : IRequestHandler<EditUserCommand, UserResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Models.User, int> _repository;

        public EditUserHandler(
            IRepository<Models.User, int> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<UserResponse> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            if (user == null) return null;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(user);

            var result = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt
            };

            await _mediator.Publish(new UserClearCacheNotification(result.Id, result), cancellationToken);

            return result;
        }
    }
}