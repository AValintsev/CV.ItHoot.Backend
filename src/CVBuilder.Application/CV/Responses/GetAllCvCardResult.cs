using CVBuilder.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.CV.Responses
{
    public class GetAllCvCardResult
    {
        public List<CvCardResult> CvCards { get; set; }
    }
}
