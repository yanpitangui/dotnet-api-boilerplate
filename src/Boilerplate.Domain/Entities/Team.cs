using Boilerplate.Domain.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities
{
    public class Team : Entity
    {
        [Required]
        public string Name { get; set; }

        public Hero TeamLeader { get; set; }

        [ForeignKey("Hero")]
        public Guid TeamLeaderId { get; set; }
    }
}
