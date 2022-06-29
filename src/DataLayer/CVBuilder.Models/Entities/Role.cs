using CVBuilder.Models.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CVBuilder.Models.Entities
{
    public class Role : IdentityRole<int>, IEntity<int>
    {
        public Role() : base() { }
        public Role(string name) : base(name) { }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<User> Users { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
