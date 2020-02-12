using Boilerplate.Domain.Core.Entities;
using Boilerplate.Domain.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public HeroType HeroType { get; set; }

        public string Team { get; set; }
    }
}
