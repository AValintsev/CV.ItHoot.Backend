using System;
using CVBuilder.Models.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Models
{
    public class User : IdentityUser<int>, IEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual string FullName => $"{FirstName} {LastName}";

        // Signing a contract
        //public bool AgreementApproved { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //[ProtectedPersonalData]
        public override string NormalizedUserName { get; set; }

        //[ProtectedPersonalData]
        public override string NormalizedEmail { get; set; }
    }
}