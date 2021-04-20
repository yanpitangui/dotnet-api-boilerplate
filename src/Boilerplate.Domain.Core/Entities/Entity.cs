using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Core.Entities
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
