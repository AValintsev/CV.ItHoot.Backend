using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBuilder.Web.Contracts.V1.Responses.File
{
    public class UploadFileRespons
    {
        public int? CvId { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
    }
}
