using System;

namespace CVBuilder.Web.Contracts.V1.Requests.Resume.SharedResumeRequest
{
     public class UpdateExperienceRequest
    {
        public int? Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
