using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Responses.CV
{
    public class GetAllCvCardResponse
    {
        public List<CvCardResponse> CvCards { get; set; }
    }
}
