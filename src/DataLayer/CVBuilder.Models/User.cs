using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities;
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

        [ProtectedPersonalData]
        public override string NormalizedUserName { get; set; }

        //[ProtectedPersonalData]
        public override string NormalizedEmail { get; set; }
        
        private List<Resume> CreatedResumes { get; set; }
        private List<Team> CreatedTeams { get; set; }
        private List<Team> ClientTeams { get; set; }
    }
}