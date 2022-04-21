using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVBuilder.Application.Education.Response;
using MediatR;

namespace CVBuilder.Application.Education.Queries
{
    public class CreateEducationComand : MediatR.IRequest<CreateEducationResult>
    {
        public int CvId { get; set; }
        public string InstitutionName { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

