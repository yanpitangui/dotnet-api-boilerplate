using Boilerplate.Domain.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities
{
    public class Team : Entity
    {
        [Required]
        public string Name { get; set; }

        public Hero TeamLeader { get; set; }

        public Guid TeamLeaderId { get; set; }
    }
}
