using Boilerplate.Domain.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Application.DTOs.Hero
{
    public class InsertHeroDTO
    {
        [Required(ErrorMessage = "É necessário informar o nome do herói")]
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int? Age { get; set; }
        public string Individuality { get; set; }
        [Required(ErrorMessage = "É necessário informar o tipo do herói")]
        public HeroType HeroType { get; set; }
        public string Team { get; set; }
    }
}
