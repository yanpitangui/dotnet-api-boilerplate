using Boilerplate.Domain.Core.Entities;
using Boilerplate.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities
{
    public class Individuality : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IndividualityType Type { get; set; }
    }
}
