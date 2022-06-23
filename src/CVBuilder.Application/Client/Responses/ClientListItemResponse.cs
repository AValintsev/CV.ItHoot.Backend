﻿namespace CVBuilder.Application.Client.Responses
{
    public class ClientListItemResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Site { get; set; }
        public string Proposals { get; set; } = "1,2,3";
        public string OtherContacts { get; set; } = "skype: @test, Telegram: @test";
        public string CompanyName { get; set; } = "Test";
    }
}