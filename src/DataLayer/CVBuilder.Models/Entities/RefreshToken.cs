using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities
{
    public class RefreshToken : IEntity<int>
    {
        public int Id { get; set; }
        public string JwtId { get; set; }
        public string Token { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ExpiryAt { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}