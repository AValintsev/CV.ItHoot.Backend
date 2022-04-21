using System;

namespace CVBuilder.Models.Entities.Interfaces
{
    public interface IDeletableEntity<TKey> : IEntity<TKey>
    {
        DateTime? DeletedAt { get; set; }
    }
}