using CVBuilder.Application.User.Responses;
using MediatR;

namespace CVBuilder.Application.User.Commands
{
    public class EditUserCommand : IRequest<UserResponse>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool NotifyByEmailUpdates { get; set; }
        public bool NotifyBySmsUpdates { get; set; }
        public bool NotifyByWhatsappUpdates { get; set; }
        public bool NotifyByEmailDailySummary { get; set; }
        public bool NotifyBySmsDailySummary { get; set; }
        public bool NotifyByWhatsappDailySummary { get; set; }
        public bool NotifyByEmailMarketingInformation { get; set; }
        public bool NotifyBySmsMarketingInformation { get; set; }
        public bool NotifyByWhatsappMarketingInformation { get; set; }
    }
}