using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.CV.Responses.CvResponses
{
    public class UserLanguageResult
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int CvId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}