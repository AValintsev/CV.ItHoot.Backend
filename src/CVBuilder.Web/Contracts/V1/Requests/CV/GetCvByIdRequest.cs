using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVBuilder.Web.Contracts.V1.Requests.CV
{
    public class GetCvByIdRequest
    {
        public int Id { get; set; }
    }
}
