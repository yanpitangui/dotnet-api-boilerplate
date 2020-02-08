using Boilerplate.Domain.Core.Entities;
using Boilerplate.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities
{
    public class Hero : Entity
    {
        [Required]
        public string Name { get; set; }
        public string Nickname { get; set; }

        public string Individuality { get; set; }

        public int Age { get; set; }
        [Required]
        public HeroType HeroType { get; set; }

        public Team Team { get; set; }
        public Guid TeamId { get; set; }

    }
}
