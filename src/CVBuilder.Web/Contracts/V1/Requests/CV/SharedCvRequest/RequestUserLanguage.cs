﻿namespace CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest
{
    public class RequestUserLanguage
    {
        public int Id { get; set; }
        public int CvId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
