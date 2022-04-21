using System;

namespace CVBuilder.Models.Entities.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}