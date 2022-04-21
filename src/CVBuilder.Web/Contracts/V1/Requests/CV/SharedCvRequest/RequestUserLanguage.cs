using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Web.Contracts.V1.Requests.CV
{
    public class RequestUserLanguage
    {
        public int Id { get; set; }
        public int CvId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
