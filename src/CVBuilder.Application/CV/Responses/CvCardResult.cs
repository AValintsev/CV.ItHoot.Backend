using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.CV.Responses
{
    public class CvCardResult
    {
        public int Id { get; set; }
        public string CvName { get; set; }
        public bool IsDraft { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
    }
}
