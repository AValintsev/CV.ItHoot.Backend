﻿namespace CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest
{
    public class UserLanguageRequest
    {
        public int? Id { get; set; }
        public int? LanguageId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
