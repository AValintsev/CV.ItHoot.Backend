using CVBuilder.Application.Expiriance.Respons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CVBuilder.Application.Expiriance.Queries
{
    public class CreateExperiencComand : IRequest<CreateExpirienceResult>
    {

        public int CvId { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
