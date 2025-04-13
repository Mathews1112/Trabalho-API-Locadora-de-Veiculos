using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models.Entities
{
    public class Fabricante
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public ICollection<Modelo> Modelos { get; set; }
    }
}