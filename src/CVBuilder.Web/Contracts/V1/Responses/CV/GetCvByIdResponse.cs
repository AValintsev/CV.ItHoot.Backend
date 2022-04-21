using CVBuilder.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBuilder.Web.Contracts.V1.Responses.CV
{
    public class GetCvByIdResponse
    {
        public Cv Cv { get; set; }
    }
}
