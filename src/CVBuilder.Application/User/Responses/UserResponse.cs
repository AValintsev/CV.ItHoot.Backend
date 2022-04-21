using System;

namespace CVBuilder.Application.User.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LegalId { get; set; }

        public bool NotifyByEmailUpdates { get; set; }
        public bool NotifyBySmsUpdates { get; set; }
        public bool NotifyByWhatsappUpdates { get; set; }
        public bool NotifyByEmailDailySummary { get; set; }
        public bool NotifyBySmsDailySummary { get; set; }
        public bool NotifyByWhatsappDailySummary { get; set; }
        public bool NotifyByEmailMarketingInformation { get; set; }
        public bool NotifyBySmsMarketingInformation { get; set; }
        public bool NotifyByWhatsappMarketingInformation { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}