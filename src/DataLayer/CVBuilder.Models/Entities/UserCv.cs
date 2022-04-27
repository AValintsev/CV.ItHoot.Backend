using CVBuilder.Models.Entities.Interfaces;
using System;

namespace CVBuilder.Models.Entities
{
    public class UserCv : IEntity<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CvId { get; set; }
        public Cv Cv { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
