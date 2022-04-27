using System;

namespace CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest
{
    public class RequestEducation
    {
        public int? Id { get; set; }
        public int CvId { get; set; }
        public string InstitutionName { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
