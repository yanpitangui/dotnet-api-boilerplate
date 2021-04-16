using System.ComponentModel.DataAnnotations;
using Boilerplate.Domain.Core.Entities;
using Boilerplate.Domain.Entities.Enums;

namespace Boilerplate.Domain.Entities
{
    public class Hero : Entity
    {
        [Required]
        public string Name { get; set; }

        public string Nickname { get; set; }
        public string Individuality { get; set; }
        public int? Age { get; set; }

        [Required]
        public HeroType? HeroType { get; set; }

        public string Team { get; set; }
    }
}
