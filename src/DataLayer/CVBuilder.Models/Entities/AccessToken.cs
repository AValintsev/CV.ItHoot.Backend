using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities
{
    public class AccessToken : IEntity<int>
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ExpiryAt { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}