using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.CV.Responses.CvResponses
{
    public class EducationResult
    {
        public int Id { get; set; }
        public int CvId { get; set; }
        public int Order { get; set; }
        public string InstitutionName { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
