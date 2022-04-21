using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBuilder.Models.Entities;

namespace CVBuilder.Web.Contracts.V1.Responses.CV
{
    public class GetAllCvCardResponse
    {
        public List<CvCardResponse> CvCards { get; set; }
    }
}
