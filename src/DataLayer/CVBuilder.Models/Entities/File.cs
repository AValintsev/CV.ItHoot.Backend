using CVBuilder.Models.Entities.Interfaces;
using System;

namespace CVBuilder.Models.Entities
{
    public class File : IEntity<int>
    {
        //[Key]
        //[ForeignKey(nameof(Cv))]
        public int Id { get; set; }
        public int? CvId { get; set; }
        //public Cv Cv { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
